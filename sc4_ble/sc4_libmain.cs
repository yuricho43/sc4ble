using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private async void RunTask(TaskName taskName, string arg1, string arg2, Action<ERROR_CODE> callback)
        {
            // 비동기로 Worker Thread에서 도는 task1
            // Task.Run(): .NET Framework 4.5+
            ERROR_CODE result = ERROR_CODE.NONE;

            switch (taskName)
            {
                case TaskName.OPEN_DEVICE:
                    result = await gBle.OpenDevice(arg1);
                    break;

                case TaskName.SET_SERVICE:
                    //task1 = Task.Run(() => bleservice.SetService(deviceName));
                    //result = await task1;
                    result = await gBle.SetService(arg2);
                    break;

                case TaskName.READ_CHARACTERISTIC:
                    {
                        var resultString = await gBle.ReadCharacteristic(arg1, arg2);
                        break;
                    }
                case TaskName.SUBSCRIBE_CHARACTERISTIC:
                    {
                        var resultString = await gBle.SubscribeCharacteristic(arg1, arg2, (arg) => { });
                        break;
                    }
                case TaskName.WRITE_CHARACTERISTIC:
                    {
                        var resultString = await gBle.WriteCharacteristic(arg1, arg2);
                        break;
                    }
            }
            callback(result);
        }

        public int SC4_Add(int a, int b)
        {
            return a + b;
        }

        public List<string> SC4_Scan_Devices()
        {
            gBle.StartScan();
            return gBle.GetDeviceList();
        }

        public string SC4_Connect_Devices(string strDevice)
        {
            ERROR_CODE result = gBle.ConnnectionStatus(strDevice);

            gDevName = strDevice;
            if (result == ERROR_CODE.BLE_CONNECTED)
                return "BLE_CONNECTED";

            RunTask(TaskName.OPEN_DEVICE, strDevice, null, (arg) => { gResult = arg.ToString(); });

            // check if connected          
            return gResult;
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
            RunTask(TaskName.SET_SERVICE, devName, svcName, (result) => { });
        }

        public void SC4_WriteCommand(string strCommand)
        {
            // parameters 
            // devName, characterName + command
            string strFinalCmd = "WriteCharacteristics " + strCommand;
            RunTask(TaskName.WRITE_CHARACTERISTIC, gDevName, strFinalCmd, (arg) => { });
        }
    }
}
