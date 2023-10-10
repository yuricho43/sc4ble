using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ble.Service;
using nrf.DFU;

namespace sc4_ble
{
    public class SC4_BleLib
    {
        public Bleservice gBle = new Bleservice();
        nrfDFU dfu = new nrfDFU();

        public string gResult;
        public static string gDevName = null;
        public static string gSvcName = null;
        public static string gCharNameWrite = null;
        public static string gCharNameNotification = null;
        public int gStatus = 0;     //0: nothing, 1: waiting, 2: completed
        public int gDFUMode = 0;
        List<string> gServiceList = null;
        int gIxService = -1;

        delegate void BleCallback(ERROR_CODE er, string resultString);  // 대리자 선언
        delegate void NotifCallback(string resultString);               // 대리자 선언
        Func<string, int> gCallback = null;

        //***************************************************************************
        private async void RunTask(TaskName taskName, string arg1, string arg2, BleCallback callback)
        {
            // 비동기로 Worker Thread에서 도는 task1
            // Task.Run(): .NET Framework 4.5+
            ERROR_CODE result = ERROR_CODE.NONE;
            string strResult = "-";

            switch (taskName)
            {
                case TaskName.OPEN_DEVICE:
                    result = await gBle.OpenDevice(arg1);
                    strResult = "Open Device";
                    break;

                case TaskName.SET_SERVICE:
                    //task1 = Task.Run(() => bleservice.SetService(deviceName));
                    //result = await task1;
                    result = await gBle.SetService(arg2);
                    strResult = "Set Service";
                    break;

                case TaskName.READ_CHARACTERISTIC:
                    {
                        var resultString = await gBle.ReadCharacteristic(arg1, arg2);
                        strResult = resultString;
                        break;
                    }
                case TaskName.SUBSCRIBE_CHARACTERISTIC:
                    {
                        // NotifCallback nc = new NotifCallback(NotifCallbackFunction);
                        System.Action<string> nc1 = new System.Action<string>(NotifCallbackFunction);
                        var resultString = await gBle.SubscribeCharacteristic(arg1, arg2, nc1);
                        strResult = resultString;
                        break;
                    }
                case TaskName.WRITE_CHARACTERISTIC:
                    {
                        var resultString = await gBle.WriteCharacteristic(arg1, arg2);
                        strResult = resultString;
                        break;
                    }
            }
            gStatus = 2;
            callback(result, strResult);
        }

        //***************************************************************************
        //--- Internal Callback from BLEService
        public void CallbackFunction(ERROR_CODE er, string result)
        {
            gResult = "[LIB]" + er.ToString() + "." + result;
            /*
             if (gCallback != null)
                gCallback(gResult);
            */
        }

        //***************************************************************************
        //--- Application Callback When device announce notification
        public void NotifCallbackFunction(string result)
        {
            string strCallback = "[NOTIF]" + result;
            if (gCallback != null)
                gCallback(strCallback);
        }

        //***************************************************************************
        //--- RunTask Thread
        public void Running_Ble_Task(object iType)
        {
            TaskName taskName = (TaskName) iType;
            BleCallback bc = new BleCallback(CallbackFunction);

            switch (taskName)
            {
                case TaskName.OPEN_DEVICE:
                    RunTask(taskName, gDevName, null, bc);
                    break;

                case TaskName.SET_SERVICE:
                    RunTask(taskName, gDevName, gSvcName, bc);
                    break;

                case TaskName.SUBSCRIBE_CHARACTERISTIC:
                    RunTask(taskName, gDevName, gCharNameNotification, bc);
                    break;
            }
        }

        //===========================================================================
        //--- 1) Set Notification Callback
        public void SC4_Set_Callback(Func<string, int> func)
        {
            gCallback = func;
            return;
        }

        //===========================================================================
        //--- 2) Scan SC4 Device
        public List<string> SC4_Scan_Devices()
        {
            List<string> lstAllDevices;
            gBle.StartScan();
            lstAllDevices = gBle.GetDeviceList();
            for (int i = 0; i < lstAllDevices.Count; i++)
            {
                if (lstAllDevices[i].Contains("SC4") == false)
                {
                    lstAllDevices.RemoveAt(i);
                    i--;
                }
            }
            return lstAllDevices;
        }

        //===========================================================================
        //--- 3) Connect Device
        public string SC4_Connect_Device(string strDevice)
        {
 
            ERROR_CODE erCon = gBle.ConnnectionStatus(strDevice);

            if (erCon == ERROR_CODE.BLE_CONNECTED && gDevName != null && gDevName == strDevice)
                return "Already Connected";

            gDevName = strDevice;

            gStatus = 1;

            // Make Thread() : call RunTask
            Thread t1 = new Thread(new ParameterizedThreadStart(Running_Ble_Task));
            t1.Start(TaskName.OPEN_DEVICE);

            // wait callback called
            while (gStatus != 2)
            {
                Thread.Sleep(300);
            }

            // check if connected, then retrieve service & characteristics
            erCon = gBle.ConnnectionStatus(strDevice);
            if (erCon == ERROR_CODE.BLE_CONNECTED)
            {

                // Get Service, Set Service
                SC4_GetServiceList();       // Get & Set Service
                Thread.Sleep(300);
                // Set Notification Subscribe
                SC4_Subscribe_Characteristics(gDevName, "");    // find notification chars and set subscribe
            }
            return gResult;
        }

        //===========================================================================
        //--- Get Library Status : Scanned, Paired, Connected, Disconnected (Idle)
        //                         SetService, SetCharacteristics
        public int SC4_GetStatus()
        {
            return gStatus;
        }

        //===========================================================================
        //--- 3-1) Get Service List
        public List<string> SC4_GetServiceList()
        {
            gServiceList = gBle.GetServiceList();

            //--- find target service list index
            for (int i = 0; i < gServiceList.Count; i++)
            {
                if (gServiceList[i].Contains("30300001-5365")) {
                    gIxService = i;
                    SC4_SetService(gDevName, gServiceList[i]);
                    break;
                }
            }
            return gServiceList;
        }

        //***************************************************************************
        //--- 3-2) Set Service List for the paired device
        private void SC4_SetService(string devName, string svcName)
        {
            gSvcName = svcName;

            gStatus = 1;
            // Make Thread() : call RunTask
            Thread t1 = new Thread(new ParameterizedThreadStart(Running_Ble_Task));
            t1.Start(TaskName.SET_SERVICE);

            // wait callback called
            while (gStatus != 2)
            {
                Thread.Sleep(200);
            }
        }

        //===========================================================================
        //===========================================================================
        //--- 3-3) Get Characteristics for the service
        public List<string> SC4_GetCharacteristicList(string svcName)
        {
            // Set Service
            if (gSvcName == null)
                SC4_SetService(gDevName, svcName);

            //--- Get Characteristics of the service
            return gBle.GetCharacteristicList();
        }


        //***************************************************************************
        private void AssignCharacteristics()
        {

            if (gSvcName == null)
            {
                SC4_GetServiceList();
            }
            List<string> listChars = SC4_GetCharacteristicList(gSvcName);
            if (listChars.Count <= 0)
                return;

            for (int i = 0; i < listChars.Count; i++)
            {
                string[] parts = listChars[i].Split(' ');
                if (listChars[i].Contains("30300002-5365"))
                {
                    gCharNameWrite = parts[0];
                }
                else if (listChars[i].Contains("30300003-5365"))
                {
                    gCharNameNotification = parts[0];
                }
            }

            // find subscribe chars
        }
        //===========================================================================
        //--- 3-4) Subscribe for the notification subscribe
        public void SC4_Subscribe_Characteristics(string devName, string strChars)
        {
            if (gCharNameNotification == null)
            {
                AssignCharacteristics();
            }

            if (gCharNameNotification != null)
            {
                gStatus = 1;

                // Make Thread() : call RunTask
                Thread t1 = new Thread(new ParameterizedThreadStart(Running_Ble_Task));
                t1.Start(TaskName.SUBSCRIBE_CHARACTERISTIC);

                // wait callback called
                while (gStatus != 2)
                {
                    Thread.Sleep(200);
                }

            }
        }

        //===========================================================================
        //--- 4) Write Command
        public void SC4_WriteCommand(string strCommand)
        {
            if (gCharNameWrite == null)
            {
                AssignCharacteristics();
            }

            // parameters 
            // devName, characterName + command
            if (gCharNameWrite != null)
            {
 
                BleCallback bc = new BleCallback(CallbackFunction);
                string strFinalCmd = gCharNameWrite + " " + strCommand;
                RunTask(TaskName.WRITE_CHARACTERISTIC, gDevName, strFinalCmd, bc);
            }
        }

        //===========================================================================
        //--- 5) Disconnect Device
        public void SC4_Disconnect()
        {
            ERROR_CODE erCon = gBle.ConnnectionStatus(gDevName);

            if (erCon == ERROR_CODE.BLE_CONNECTED)
                gBle.CloseDevice();

            gDevName = null;
            gSvcName = null;
            gCharNameWrite = null;
            gCharNameNotification = null;
            gServiceList = null;
        }

        //===========================================================================
        //--- 6) DFU
        public async void SC4_DFU(string dfu_ap_dat_file_name, string dfu_ap_bin_file_name)
        {

            string strDFU = "53 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 45 E8";
            SC4_WriteCommand(strDFU);

            gDevName = null;        // change status to disconnected
            gCharNameWrite = null;
            gCharNameNotification = null;

            gDFUMode = 1;            // DFU 
            NRF_ERROR_CODE result;
            string parameters = "SC4 DFU";

            // only allow for one connection to be open at a time
            if (gCallback != null)
                gCallback("[LIB] SC4 DFU Started");

            result = await dfu.Open(parameters);
            if (!result.Equals(NRF_ERROR_CODE.NRF_FOUND_DEVICE))
            {
                if (gCallback != null)
                    gCallback("[LIB]" + $"Open Error:  {result}");
                gDFUMode = 0;
                return;
            }
            bool noti_result = await dfu.dfu_set_packet_receive_notification(0);
            if (noti_result)
            {
                if (gCallback != null)
                    gCallback("[LIB]" + $"dfu_set_packet_receive_notification Success");
            }
            else
            {
                if (gCallback != null)
                    gCallback("[LIB]" + $"dfu_set_packet_receive_notification Failed");
            }
            ///////////////////////////////////////////////////////

            if (gCallback != null)
                gCallback("[LIB] Sending Init");

            result = await dfu.dfu_object_write_procedure(1, dfu_ap_dat_file_name);
            if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
            {
                if (gCallback != null)
                    gCallback("[LIB]" + $"dfu_object_write_procedure: Error {dfu_ap_dat_file_name}");
                gDFUMode = 0;
                return;
            }
            if (gCallback != null)
            {
                gCallback("[LIB] Init Done.");
                gCallback("[LIB] Sending Data... Wait for around 60 seconds.");
            }

            result = await dfu.dfu_object_write_procedure(2, dfu_ap_bin_file_name);
            if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
            {
                if (gCallback != null)
                    gCallback("[LIB]" + $"dfu_object_write_procedure: Error {dfu_ap_bin_file_name}");
                gDFUMode = 0;
                return;
            }

            if (gCallback != null)
                gCallback("[LIB] SC4 DFU Finished. Reboot SC4 Device.");

            gBle.CloseDevice();
            gDFUMode = 0;
        }

        public int SC4_Is_Connected(string strDevice)
        {
            if (gDevName == null)
                return -1;

            ERROR_CODE erCon = gBle.ConnnectionStatus(strDevice);
            if (erCon == ERROR_CODE.BLE_CONNECTED)
                return 0;

            return -1;
        }

        public int SC4_Get_DFU_Progress()
        {
            if (gDFUMode != 1)   // not dfu mode
                return 0;

            int progress = (int) dfu.dfu_object_write_procedure_progress();
            return progress;
        }
    }
}
