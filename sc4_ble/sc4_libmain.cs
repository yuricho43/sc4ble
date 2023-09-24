using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ble.Service;

namespace sc4_ble
{
    public class SC4_BleLib
    {
        public Bleservice gBle = new Bleservice();
        public string gResult;
        public static string gDevName = null;
        public static string gSvcName = null;
        public static string gCharName = null;
        public int gStatus = 0;     //0: nothing, 1: waiting, 2: completed
        public ListBox gListResponse, gListDebug, gListNotification;

        delegate void BleCallback(ERROR_CODE er, string resultString);  // 대리자 선언
        delegate void NotifCallback(string resultString);               // 대리자 선언

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
            callback(result, strResult);
            gStatus = 2;
        }


        public void Set_ListBox(ListBox listResponse, ListBox listNotification, ListBox listDebug)
        {
            gListResponse = listResponse;
            gListNotification = listNotification;
            gListDebug = listDebug;
        }

        public List<string> SC4_Scan_Devices()
        {
            gBle.StartScan();
            return gBle.GetDeviceList();
        }

        public string SC4_Connect_Devices(string strDevice)
        {
            gDevName = strDevice;

            /*
             * ERROR_CODE result;
             Task<ERROR_CODE> t1 =gBle.OpenDevice(strDevice);
             result = t1.Result;

             if (result == ERROR_CODE.BLE_CONNECTED)
                 return ERROR_CODE.BLE_CONNECTED.ToString();
            */
            BleCallback bc = new BleCallback(CallbackFunction);
            RunTask(TaskName.OPEN_DEVICE, strDevice, null, bc);
            gStatus = 0;

            // check if connected          
            return "Connecting...";
        }

        public int SC4_GetStatus()
        {
            return gStatus;
        }

        public List<string> SC4_GetServiceList()
        {
            return gBle.GetServiceList();
        }

        
        public List<string> SC4_GetCharacteristicList(string svcName)
        {
            // Set Service
            SC4_SetService(gDevName, svcName);

            //--- Get Characteristics of the service
            return gBle.GetCharacteristicList();
        }

        public void SC4_SetService(string devName, string svcName)
        {
            gSvcName = svcName;
            BleCallback bc = new BleCallback(CallbackFunction);
            RunTask(TaskName.SET_SERVICE, devName, svcName, bc);
        }

        public void SC4_Subscribe_Characteristics(string devName, string strChars)
        {
            BleCallback bc = new BleCallback(CallbackFunction);
            RunTask(TaskName.SUBSCRIBE_CHARACTERISTIC, devName, strChars, bc);
        }

        public void SC4_WriteCommand(string strChars, string strCommand)
        {
            // parameters 
            // devName, characterName + command
            BleCallback bc = new BleCallback(CallbackFunction);
            string strFinalCmd = strChars + " " + strCommand;            
            RunTask(TaskName.WRITE_CHARACTERISTIC, gDevName, strFinalCmd, bc);
        }

        public void CallbackFunction(ERROR_CODE er, string result)
        {
            string strCallback = er.ToString() + ": " + result;
            Console.WriteLine(strCallback);

            gListDebug.Items.Add(strCallback);
        }
        public void NotifCallbackFunction(string result)
        {
            string strCallback = "NOTIF:" + result;
            gListDebug.Invoke((MethodInvoker)delegate ()
            {
                gListDebug.Items.Add(strCallback);
            });

        }
    }
}
