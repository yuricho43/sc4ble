using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc4_ble;

namespace SC4_SDK_App
{
    public partial class FormSC4 : Form
    {
        SC4_BleLib gSC4Lib = new SC4_BleLib();
        byte[] gCommand = new byte[20];

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
            //gCommand[6~9] = float.Parse(textSetTeeHight.Text);        // TeeHight
            //gCommand[10~13]=float.Parse(textSetDistanceToBall.Text);  // Distance To Ball
            gCommand[14] = byte.Parse(textSetTargetDistance.Text);      // TargetDistance
            gCommand[15] = (byte)cmbSetClub.SelectedIndex;              // Club
            //gCommand[16 - 17] = UInt16.Parse(textSetLoftAngle.Text);  // Loft Angle in units of 0.1
            gCommand[18] = 0x45;                                        // End
            gCommand[19] = GetCheckSum(gCommand, 19);                   // CheckSum

            //--- array to string
            string strCmd = ConvertHexArrayToString(gCommand, 20);

            //--- write to textCommand TextBox
            textCommand.Text = strCmd;
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
            gSC4Lib.Set_ListBox(listReponse, listNotfication, listDebug);
            gSC4Lib.Set_Callback(Process_Notification_Callback);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            comboDeviceList.Items.Clear();

            var listDevice = gSC4Lib.SC4_Scan_Devices();
            for (int i = 0; i < listDevice.Count(); i++)
            {
                comboDeviceList.Items.Add($"{listDevice[i]}");
            }

            AddToDebugList("Scan Completed. " + listDevice.Count().ToString() + " devices found");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string strResult;
            //--- connect to devices scanned
            string strDevice = comboDeviceList.Text.ToString();
            strResult = gSC4Lib.SC4_Connect_Devices(strDevice);
            AddToDebugList("Try to Connect. " + " Result=" + strResult);
        }

        private void btnGetSvc_Click(object sender, EventArgs e)
        {
            comboServiceList.Items.Clear();
            var result = gSC4Lib.SC4_GetServiceList();
            for (int i = 0; i < result.Count(); i++)
            {
                comboServiceList.Items.Add($"{result[i]}");
            }
        }

        private void btnGetChar_Click(object sender, EventArgs e)
        {
            string strDevice = comboDeviceList.Text.ToString();
            string strSvc = comboServiceList.Text.ToString();

            // Get Characteristics (including Selection of Service)
            var result = gSC4Lib.SC4_GetCharacteristicList(strSvc);

            listCharList.Items.Clear();
            for (int i = 0; i < result.Count(); i++)
            {
                listCharList.Items.Add($"{result[i]}");
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string strCommand = textCommand.Text.ToString();
            var parts = listCharList.Text.ToString().Split(' ');
            string strChars = parts[0];
            gSC4Lib.SC4_WriteCommand(strChars, strCommand);
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

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            string strDevice = comboDeviceList.Text.ToString();
            string strSvc = comboServiceList.Text.ToString();
            var parts = listCharList.Text.ToString().Split(' ');
            string strChars = parts[0];
            gSC4Lib.SC4_Subscribe_Characteristics(strDevice, strChars);
        }

        //--- callback function
        public int Process_Notification_Callback(string strResult)
        {

            listNotfication.Invoke((MethodInvoker)delegate ()
            {
                listNotfication.Items.Add(strResult);
            });

            return 0;
        }
    }
}
