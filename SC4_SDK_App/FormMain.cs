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

        //--- Make Setting Value (App --> Dev)
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // 1~5  : 0x53 0x6F Flag  Cary/Total   Mode
            // 6~10 :Unit TeeHight
            // 11-15: Distance to Ball, Target Distance
            // 16~20: Club, LoftAngle, 0x45, CheckSum

            gCommand[0] = 0x53;                                         // Start
            gCommand[1] = 0x6F;                                         // CMD
            gCommand[2] = 0;                                            // Flag
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
            //--- write to textCommand TextBox
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

            //--- test routine 
            int iVal = gSC4Lib.SC4_Add(4, 4);
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
            AddToDebugList("Connect Completed. " + " Result=" + strResult);
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
            gSC4Lib.SC4_WriteCommand(strCommand);
        }
    }
}
