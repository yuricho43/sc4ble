using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ble.Service;
using sc4_ble;

namespace SC4_SDK_App
{
    public partial class FormSC4 : Form
    {
        SC4_BleLib gSC4Lib = new SC4_BleLib();
        byte[] gCommand = new byte[20];
        int giDisconnected = -1;

        public string ConvertHexArrayToString(byte[] data, int len)
        {
            byte[] byteArray = new byte[len];
            Buffer.BlockCopy(data, 0, byteArray, 0, len);

            string strResult = System.BitConverter.ToString(byteArray);
            strResult = strResult.Replace("-", " ");
            return strResult;
        }

        //--- Make Setting Value (App --> Dev)
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            UInt16 uloft = (UInt16)(float.Parse(textSetLoftAngle.Text)*10);
            float fTee = float.Parse(textSetTeeHight.Text);
            float fDistance = float.Parse(textSetDistanceToBall.Text);

            byte[] bTee = new byte[4];
            byte[] bDist = new byte[4];
            bTee = BitConverter.GetBytes(fTee);
            bDist = BitConverter.GetBytes(fDistance);

            // 1~5  : 0x53 0x6F Flag  Cary/Total   Mode
            // 6~10 :Unit TeeHight
            // 11-15: Distance to Ball, Target Distance
            // 16~20: Club, LoftAngle, 0x45, CheckSum

            // Flag : bit7  bit6  bit5  bit4     bit3  bit2  bit1  bit0
            //        Carry Loft  club  target   dist  tee   Unit  Mode
            Array.Clear(gCommand, 0, gCommand.Length);
            if (chkSetMode.Checked)
                gCommand[2] |= 0x01;
            if (chkSetUnit.Checked)
                gCommand[2] |= 0x02;
            if (chkSetTeeHight.Checked)
                gCommand[2] |= 0x04;
            if (chkSetDistanceToBall.Checked)
                gCommand[2] |= 0x08;
            if (chkSetTargetDistance.Checked)
                gCommand[2] |= 0x10;
            if (chkSetClub.Checked)
                gCommand[2] |= 0x20;
            if (chkSetLoftAngle.Checked)
                gCommand[2] |= 0x40;
            if (chkSetCarryTotal.Checked)
                gCommand[2] |= 0x80;

            gCommand[0] = 0x53;                                         // Start
            gCommand[1] = 0x6F;                                         // CMD
            gCommand[3] = (byte)cmbSetCarryTotal.SelectedIndex;         // CarryTotal
            gCommand[4] = (byte)cmbSetMode.SelectedIndex;               // Mode
            gCommand[5] = (byte)cmbSetUnit.SelectedIndex;               // Unit
            for (int i = 0; i < 4; i++)
            {
                gCommand[6 + i] = bTee[i];                              // TeeHight
                gCommand[10 + i] = bDist[i];                            // Distance To Ball
            }
            gCommand[14] = byte.Parse(textSetTargetDistance.Text);      // TargetDistance
            gCommand[15] = (byte)cmbSetClub.SelectedIndex;              // Club
            gCommand[16] = (byte)(uloft & 0xFF);                        // Loft Angle in units of 0.1
            gCommand[17] = (byte)((uloft & 0xFF00) >> 8);               // Loft Angle in units of 0.1
            gCommand[18] = 0x45;                                        // End
            gCommand[19] = GetCheckSum(gCommand, 19);                   // CheckSum

            //--- array to string
            string strCmd = ConvertHexArrayToString(gCommand, 20);

            //--- write to textCommand TextBox
            textCommand.Text = strCmd;
        }

        private void Enable_Controls(int iDisconnected)
        {
            //--- Connected
            if (iDisconnected == 0)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnWrite.Enabled = true;
                btnDFU.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnWrite.Enabled = false;
                btnDFU.Enabled = false;
            }
        }   
                
        private byte GetCheckSum(byte[] data, int length)
        {
            byte bValue = 0x00;
            byte bCheckSum = 0;
            UInt16 bTemp = (UInt16)0x100;
            for (int i = 0; i < length; i++)
                bValue += data[i];

            //--- 2'c complementary
            bCheckSum = (byte)(bTemp - bValue);
            return bCheckSum;
        }

        private void AddToDebugList(string strMsg)
        {
            listDebug.Items.Add(strMsg);
        }

        public FormSC4()
        {
            InitializeComponent();
            gSC4Lib.SC4_Set_Callback(Process_Notification_Callback);
            timerConnect.Enabled = true;
            Enable_Controls(giDisconnected);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            comboDeviceList.Items.Clear();

            var listDevice = gSC4Lib.SC4_Scan_Devices();
            for (int i = 0; i < listDevice.Count(); i++)
            {
                comboDeviceList.Items.Add($"{listDevice[i]}");
            }

            comboDeviceList.SelectedIndex = 0;
            AddToDebugList("[App]Scan Completed. " + listDevice.Count().ToString() + " devices found");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strResult;
            //--- connect to devices scanned
            string strDevice = comboDeviceList.Text.ToString();
            strResult = gSC4Lib.SC4_Connect_Device(strDevice);
            AddToDebugList("[App:Connect] " + " Result=" + strResult);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string strCommand = textCommand.Text.ToString();
            gSC4Lib.SC4_WriteCommand(strCommand);
        }

        private void btnDevInfo_Click(object sender, EventArgs e)
        {
            // Make Device Info String
            // 0x53, 0x64, 16x 0x00, 0x45, checksum
            Array.Clear(gCommand, 0, gCommand.Length);
            gCommand[0] = 0x53;                                         // Start
            gCommand[1] = 0x64;                                         // CMD
            gCommand[18] = 0x45;                                        // End
            gCommand[19] = GetCheckSum(gCommand, 19);                   // CheckSum

            //--- array to string
            string strCmd = ConvertHexArrayToString(gCommand, 20);
            //--- write to textCommand TextBox
            textCommand.Text = strCmd;
        }

        //--- callback function
        public int Process_Notification_Callback(string strResult)
        {
            listDebug.Invoke((MethodInvoker)delegate ()
            {
                listDebug.Items.Add(strResult);
            });
            Console.WriteLine(strResult);

            return 0;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            gSC4Lib.SC4_Disconnect();
        }

        private void timerConnect_Tick(object sender, EventArgs e)
        {
            string strDevice = comboDeviceList.Text.ToString();
            giDisconnected = gSC4Lib.SC4_Is_Connected(strDevice);

            if (giDisconnected == 0)
                btnConnect.BackColor = Color.LightGreen;
            else
                btnConnect.BackColor = Color.Red;

            Enable_Controls(giDisconnected);
        }

        private void btnDat_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.FileName = "ble_app_uart_pca10040_s132.dat";
            openFileDialog1.Filter = "dat file|*.dat";
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textDat.Text = openFileDialog1.FileName;
            }
        }

        private void btnBin_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.FileName = "ble_app_uart_pca10040_s132.bin";
            openFileDialog1.Filter = "dat file|*.bin";
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBin.Text = openFileDialog1.FileName;
            }
        }

        public void Execute_DFU_Thread(object args)
        {
            Array argArray = new object[2];
            argArray = (Array)args;
            string strDat = (string)argArray.GetValue(0);
            string strBin = (string)argArray.GetValue(1);

            gSC4Lib.SC4_DFU(strDat, strBin);
        }

        private void btnDFU_Click(object sender, EventArgs e)
        {

            string dfu_ap_dat_file_name, dfu_ap_bin_file_name;
            dfu_ap_dat_file_name = textDat.Text.ToString();
            dfu_ap_bin_file_name = textBin.Text.ToString();
            object args = new object[2] { dfu_ap_dat_file_name, dfu_ap_bin_file_name};

            timerDFU.Enabled = true;

            //--- because DFU is sync process, you should start with thread if you want get progress.
            Thread t1 = new Thread(new ParameterizedThreadStart(Execute_DFU_Thread));
            t1.Start(args);

            // gSC4Lib.SC4_DFU(dfu_ap_dat_file_name, dfu_ap_bin_file_name);
            ///////////////////////////////////////////////////////
        }

        private void timerDFU_Tick(object sender, EventArgs e)
        {
            int progress = gSC4Lib.SC4_Get_DFU_Progress();
            progressBar1.Value = progress;

            Console.WriteLine(progress.ToString());

            if (progress > 95)
            {
                progressBar1.Value = 100;
                timerDFU.Enabled = false;
            }
        }
    }
}
