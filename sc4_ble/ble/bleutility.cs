﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using System.Threading;



namespace Ble.Service
{

    static class Constants
    {
        public const string MFG_VC_UUID = "2d420000-6569-6464-6163-6563696f562d";
        public const string SC4_NAME_PREFIX = "SC4";
        public const string SC4_NAME_DFU = "SC4 DFU";
    }
    public enum TaskName : int
    {
        OPEN_DEVICE,
        SET_SERVICE,
        READ_CHARACTERISTIC,
        SUBSCRIBE_CHARACTERISTIC,
        WRITE_CHARACTERISTIC,
        NO_TASK,
    }
    public enum ERROR_CODE : int
    {
        // ERROR_NONE
        NONE,
        CMD_WRONG_PARAMETER,
        // INFORMATION ENUM
        BLE_FOUND_DEVICE,
        BLE_CONNECTED,
        BLE_NO_CONNECTED,
        
        // ERROR ENUM
        NO_SELECTED_SERVICE,
        UNKNOWN_ERROR,

        
        // ERROR CATEGORY
        // OPENDEVICE
        OPENDEVICE_UNREACHABLE,
        OPENDEVICE_UNKNOWN,

        // SETSERVICE
        SERVICE_NO_SERVICE,
        SERVICE_ACCESS_ERROR,
        SERVICE_NOT_GRANT_ACCEWSS,
        SERVICE_CANOT_READ_CHAR,
        SERVICE_INVALID_SERVICE,
        

        // READCHARACTERISTIC
        READ_FAIL,
        READ_INVALID_CHARACTERISTIC,
        READ_NOTHING_TO_READ,
        READ_EXCEPTION_1,
        READ_EXCEPTION_2,
        READ_EXCEPTION_3,

        // SUBSCRIBECHARACTERISTIC
        SUBSCRIBE_FAIL,
        SUBSCRIBE_INVALID_CHARACTERISTIC,
        SUBSCRIBE_NOTHING_TO_READ,
        SUBSCRIBE_NOT_SUPPORT_NOTIFY_INDICATE,
        SUBSCRIBE_CANNOT_SUBSCRIBE,
        SUBSCRIBE_ALREADY_CHARACTERISTIC,
        SUBSCRIBE_EXCEPTION_1,
        SUBSCRIBE_EXCEPTION_2,
        SUBSCRIBE_EXCEPTION_3,

        // WRITEHARACTERISTIC
        WRITE_FAIL,
        WRITE_INVALID_CHARACTERISTIC,
        WRITE_NOTHING_TO_WRITE,
        WRITE_EXCEPTION_1,
        WRITE_EXCEPTION_2,
        WRITE_EXCEPTION_3,

        // Pair
        PAIRED_BLE,
        PAIRING_ALREADY_CONNECTED,
        PAIRING_SUCCESS,
        PAIRING_NOT_READY_TO_PAIR,
        PAIRING_NOT_PAIRED,
        PAIRING_ALREADY_PAIRED,
        PAIRING_CONNECTION_REJECTED,
        PAIRING_TOO_MANY_CONNECTIONS,
        PAIRING_HARDWARE_FAILURE,
        PAIRING_AUTHENTICATION_TIMEOUT,
        PAIRING_AUTHENTICATIION_NOT_ALLOWED,
        PAIRING_AUTHENTICATION_FAILURE,
        PAIRING_NOT_SUPPORTED_PROFILES,
        PAIRING_PROTEDTION_LEVEL_COULDNOT_BE_MET,
        PAIRING_ACCESS_DENIED,
        PAIRING_INVALID_CEREMONYDATA,
        PAIRING_PAIRING_CANCELED,
        PAIRING_OPERATION_ALREADY_INPROGRESS,
        PAIRING_REQUIRED_HANDLER_NOT_REGISTERED,
        PAIRING_REJECTED_BY_HANDLER,
        PAIRING_REMOTE_DEVICE_HAS_ASSOCIATION,
        PAIRING_FAILED,

        // Unpair
        UNPAIRED_SUCCESS,
        UNPAIR_FAILED_DISCONNECTED,
        ALREADY_UNPAIRED,
        UNPAIRE_ALREADY_INPROGRESS,
        UNPAIRE_FAILED,

    }
    ///     Represents the display of an attribute - both characteristics and services.
    public class BluetoothLEAttributeDisplay
    {
        public GattCharacteristic characteristic;
        public GattDescriptor descriptor;

        public GattDeviceService service;

        public BluetoothLEAttributeDisplay(GattDeviceService service)
        {
            this.service = service;
            AttributeDisplayType = AttributeType.Service;
        }

        public BluetoothLEAttributeDisplay(GattCharacteristic characteristic)
        {
            this.characteristic = characteristic;
            AttributeDisplayType = AttributeType.Characteristic;
        }

        public string Chars => (CanRead ? "R" : " ") + (CanWrite ? "W" : " ") + (CanNotify ? "N" : " ");

        public bool CanRead
        {
            get
            {
                return this.characteristic != null ? this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read) : false;
            }
        }

        public bool CanWrite
        {
            get
            {
                return this.characteristic != null ?
                    (this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write) ||
                        this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse) ||
                        this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites) ||
                        this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WritableAuxiliaries))
                    : false;
            }
        }

        public bool CanNotify
        {
            get
            {
                return this.characteristic != null ? this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify) : false;
            }
        }
        public bool CanIndicate
        {
            get
            {
                return this.characteristic != null ? this.characteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Indicate) : false;
            }
        }

        public string Name
        {
            get
            {
                switch (AttributeDisplayType)
                {
                    case AttributeType.Service:
                        if (IsSigDefinedUuid(service.Uuid))
                        {
                            GattNativeServiceUuid serviceName;
                            if (Enum.TryParse(Utilities.ConvertUuidToShortId(service.Uuid).ToString(), out serviceName))
                            {
                                return serviceName.ToString();
                            }
                        }
                        else if (IsSigMfgVCUuid(service.Uuid))
                        {
                            GattMfgVCServiceUuid serviceName;
                            if (Enum.TryParse(Utilities.ConvertUuidToShortId(service.Uuid).ToString(), out serviceName))
                            {
                                return serviceName.ToString();
                            }
                        }
                        else
                        {
                            return $"{service.Uuid}";
                        }
                        break;
                    case AttributeType.Characteristic:
                        if (IsSigDefinedUuid(characteristic.Uuid))
                        {
                            GattNativeCharacteristicUuid characteristicName;
                            if (Enum.TryParse(Utilities.ConvertUuidToShortId(characteristic.Uuid).ToString(),
                                out characteristicName))
                            {
                                return characteristicName.ToString();
                            }
                        }
                        else if (IsSigMfgVCUuid(characteristic.Uuid))
                        {
                            GattMfgVCCharacteristicUuid characteristicName;
                            if (Enum.TryParse(Utilities.ConvertUuidToShortId(characteristic.Uuid).ToString(),
                                out characteristicName))
                            {
                                return characteristicName.ToString();
                            }
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty(characteristic.UserDescription))
                            {
                                return characteristic.UserDescription;
                            }

                            else
                            {
                                return $"{characteristic.Uuid}";
                            }
                        }
                        break;
                    default:
                        break;
                }
                return "Invalid";
            }
        }

        public AttributeType AttributeDisplayType { get; }

        /// <summary>
        ///     The SIG has a standard base value for Assigned UUIDs. In order to determine if a UUID is SIG defined,
        ///     zero out the unique section and compare the base sections.
        /// </summary>
        /// <param name="uuid">The UUID to determine if SIG assigned</param>
        /// <returns></returns>
        private static bool IsSigDefinedUuid(Guid uuid)
        {
            var bluetoothBaseUuid = new Guid("00000000-0000-1000-8000-00805F9B34FB");

            var bytes = uuid.ToByteArray();
            // Zero out the first and second bytes
            // Note how each byte gets flipped in a section - 1234 becomes 34 12
            // Example Guid: 35918bc9-1234-40ea-9779-889d79b753f0
            //                   ^^^^
            // bytes output = C9 8B 91 35 34 12 EA 40 97 79 88 9D 79 B7 53 F0
            //                ^^ ^^
            bytes[0] = 0;
            bytes[1] = 0;
            var baseUuid = new Guid(bytes);
            return baseUuid == bluetoothBaseUuid;
        }

        private static bool IsSigMfgVCUuid(Guid uuid)
        {
            var bluetoothBaseUuid = new Guid(Constants.MFG_VC_UUID);

            var bytes = uuid.ToByteArray();
            // Zero out the first and second bytes
            // Note how each byte gets flipped in a section - 1234 becomes 34 12
            // Example Guid: 35918bc9-1234-40ea-9779-889d79b753f0
            // Example Guid: 2d421523-6569-6464-6163-6563696f562d
            //                   ^^^^
            // bytes output = 23 15 42 2d 69 65 64 64 63 61 65 63 69 6f 56 2d
            //                ^^ ^^
            bytes[0] = 0;
            bytes[1] = 0;
            var baseUuid = new Guid(bytes);
            return baseUuid == bluetoothBaseUuid;
        }
    }

    public enum AttributeType
    {
        Service = 0,
        Characteristic = 1,
        Descriptor = 2
    }

    /// <summary>
    ///     Display class used to represent a BluetoothLEDevice in the Device list
    /// </summary>
    public class BluetoothLEDeviceDisplay : INotifyPropertyChanged
    {
        public BluetoothLEDeviceDisplay(DeviceInformation deviceInfoIn)
        {
            DeviceInformation = deviceInfoIn;
        }

        public DeviceInformation DeviceInformation { get; private set; }

        public string Id => DeviceInformation.Id;
        public string Name => DeviceInformation.Name;
        public bool IsPaired => DeviceInformation.Pairing.IsPaired;
        public bool IsConnected => (bool?)DeviceInformation.Properties["System.Devices.Aep.IsConnected"] == true;
        public bool IsConnectable => (bool?)DeviceInformation.Properties["System.Devices.Aep.Bluetooth.Le.IsConnectable"] == true;

        public IReadOnlyDictionary<string, object> Properties => DeviceInformation.Properties;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Update(DeviceInformationUpdate deviceInfoUpdate)
        {
            DeviceInformation.Update(deviceInfoUpdate);

            OnPropertyChanged("Id");
            OnPropertyChanged("Name");
            OnPropertyChanged("DeviceInformation");
            OnPropertyChanged("IsPaired");
            OnPropertyChanged("IsConnected");
            OnPropertyChanged("Properties");
            OnPropertyChanged("IsConnectable");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    ///     This enum assists in finding a string representation of a BT SIG assigned value for Service UUIDS
    ///     Reference: https://developer.bluetooth.org/gatt/services/Pages/ServicesHome.aspx
    public enum GattNativeServiceUuid : ushort
    {
        None = 0,
        AlertNotification = 0x1811,
        Battery = 0x180F,
        BloodPressure = 0x1810,
        CurrentTimeService = 0x1805,
        CyclingSpeedandCadence = 0x1816,
        DeviceInformation = 0x180A,
        GenericAccess = 0x1800,
        GenericAttribute = 0x1801,
        Glucose = 0x1808,
        HealthThermometer = 0x1809,
        HeartRate = 0x180D,
        HumanInterfaceDevice = 0x1812,
        ImmediateAlert = 0x1802,
        LinkLoss = 0x1803,
        NextDSTChange = 0x1807,
        PhoneAlertStatus = 0x180E,
        ReferenceTimeUpdateService = 0x1806,
        RunningSpeedandCadence = 0x1814,
        ScanParameters = 0x1813,
        TxPower = 0x1804,
        EnvironmentalSensing = 0x181A,
        SimpleKeyService = 0xFFE0
    }

    ///     This enum is nice for finding a string representation of a BT SIG assigned value for Characteristic UUIDs
    ///     Reference: https://developer.bluetooth.org/gatt/characteristics/Pages/CharacteristicsHome.aspx
    public enum GattNativeCharacteristicUuid : ushort
    {
        None = 0,
        AlertCategoryID = 0x2A43,
        AlertCategoryIDBitMask = 0x2A42,
        AlertLevel = 0x2A06,
        AlertNotificationControlPoint = 0x2A44,
        AlertStatus = 0x2A3F,
        Appearance = 0x2A01,
        BatteryLevel = 0x2A19,
        BloodPressureFeature = 0x2A49,
        BloodPressureMeasurement = 0x2A35,
        BodySensorLocation = 0x2A38,
        BootKeyboardInputReport = 0x2A22,
        BootKeyboardOutputReport = 0x2A32,
        BootMouseInputReport = 0x2A33,
        CSCFeature = 0x2A5C,
        CSCMeasurement = 0x2A5B,
        CurrentTime = 0x2A2B,
        DateTime = 0x2A08,
        DayDateTime = 0x2A0A,
        DayofWeek = 0x2A09,
        DeviceName = 0x2A00,
        DSTOffset = 0x2A0D,
        ExactTime256 = 0x2A0C,
        FirmwareRevisionString = 0x2A26,
        GlucoseFeature = 0x2A51,
        GlucoseMeasurement = 0x2A18,
        GlucoseMeasurementContext = 0x2A34,
        HardwareRevisionString = 0x2A27,
        HeartRateControlPoint = 0x2A39,
        HeartRateMeasurement = 0x2A37,
        HIDControlPoint = 0x2A4C,
        HIDInformation = 0x2A4A,
        IEEE11073_20601RegulatoryCertificationDataList = 0x2A2A,
        IntermediateCuffPressure = 0x2A36,
        IntermediateTemperature = 0x2A1E,
        LocalTimeInformation = 0x2A0F,
        ManufacturerNameString = 0x2A29,
        MeasurementInterval = 0x2A21,
        ModelNumberString = 0x2A24,
        NewAlert = 0x2A46,
        PeripheralPreferredConnectionParameters = 0x2A04,
        PeripheralPrivacyFlag = 0x2A02,
        PnPID = 0x2A50,
        ProtocolMode = 0x2A4E,
        ReconnectionAddress = 0x2A03,
        RecordAccessControlPoint = 0x2A52,
        ReferenceTimeInformation = 0x2A14,
        Report = 0x2A4D,
        ReportMap = 0x2A4B,
        RingerControlPoint = 0x2A40,
        RingerSetting = 0x2A41,
        RSCFeature = 0x2A54,
        RSCMeasurement = 0x2A53,
        SCControlPoint = 0x2A55,
        ScanIntervalWindow = 0x2A4F,
        ScanRefresh = 0x2A31,
        SensorLocation = 0x2A5D,
        SerialNumberString = 0x2A25,
        ServiceChanged = 0x2A05,
        SoftwareRevisionString = 0x2A28,
        SupportedNewAlertCategory = 0x2A47,
        SupportedUnreadAlertCategory = 0x2A48,
        SystemID = 0x2A23,
        TemperatureMeasurement = 0x2A1C,
        TemperatureType = 0x2A1D,
        TimeAccuracy = 0x2A12,
        TimeSource = 0x2A13,
        TimeUpdateControlPoint = 0x2A16,
        TimeUpdateState = 0x2A17,
        TimewithDST = 0x2A11,
        TimeZone = 0x2A0E,
        TxPowerLevel = 0x2A07,
        UnreadAlertStatus = 0x2A45,
        AggregateInput = 0x2A5A,
        AnalogInput = 0x2A58,
        AnalogOutput = 0x2A59,
        CyclingPowerControlPoint = 0x2A66,
        CyclingPowerFeature = 0x2A65,
        CyclingPowerMeasurement = 0x2A63,
        CyclingPowerVector = 0x2A64,
        Temperature = 0x2A6E,
        TVOC = 0x9003,
        Humidity = 0x2A6F,
        Pressure = 0x2A6D,
        DigitalInput = 0x2A56,
        DigitalOutput = 0x2A57,
        ExactTime100 = 0x2A0B,
        LNControlPoint = 0x2A6B,
        LNFeature = 0x2A6A,
        LocationandSpeed = 0x2A67,
        Navigation = 0x2A68,
        NetworkAvailability = 0x2A3E,
        PositionQuality = 0x2A69,
        ScientificTemperatureinCelsius = 0x2A3C,
        SecondaryTimeZone = 0x2A10,
        String = 0x2A3D,
        TemperatureinCelsius = 0x2A1F,
        TemperatureinFahrenheit = 0x2A20,
        TimeBroadcast = 0x2A15,
        BatteryLevelState = 0x2A1B,
        BatteryPowerState = 0x2A1A,
        PulseOximetryContinuousMeasurement = 0x2A5F,
        PulseOximetryControlPoint = 0x2A62,
        PulseOximetryFeatures = 0x2A61,
        PulseOximetryPulsatileEvent = 0x2A60,
        SimpleKeyState = 0xFFE1,
        Co2 = 0x9100
    }

    // Manufacture Specific Service UUID
    // Manufacture: VC
    public enum GattMfgVCServiceUuid : ushort
    {
        None = 0,
        VCService = 0x1523,
    }
    // Manufacture Specific Chracteristic UUID
    // Manufacture: VC
    public enum GattMfgVCCharacteristicUuid : ushort
    {
        None = 0,
        FWUpdate = 0x9000,
        FanControl = 0x9001,
        ModeControl = 0x9002,
        FanSpeed = 0x9004,
        FanStatus = 0x1524,
    }
    ///     This enum assists in finding a string representation of a BT SIG assigned value for Descriptor UUIDs
    ///     Reference: https://developer.bluetooth.org/gatt/descriptors/Pages/DescriptorsHomePage.aspx
    public enum GattNativeDescriptorUuid : ushort
    {
        CharacteristicExtendedProperties = 0x2900,
        CharacteristicUserDescription = 0x2901,
        ClientCharacteristicConfiguration = 0x2902,
        ServerCharacteristicConfiguration = 0x2903,
        CharacteristicPresentationFormat = 0x2904,
        CharacteristicAggregateFormat = 0x2905,
        ValidRange = 0x2906,
        ExternalReportReference = 0x2907,
        ReportReference = 0x2908
    }

    public enum DataFormat
    {
        ASCII = 0,
        UTF8,
        Dec,
        Hex,
        Bin,
        BYTEARRAY,
    }

    public enum ProtocolErrorCode : byte
    {
        Invalid_Handle = 0x01,
        Read_Not_Permitted = 0x02,
        Write_Not_Permitted = 0x03,
        Invalid_PDU = 0x04,
        Insufficient_Authentication = 0x05,
        Request_Not_Supported = 0x06,
        Invalid_Offset = 0x07,
        Insufficient_Authorization = 0x08,
        Prepare_Queue_Full = 0x09,
        Attribute_Not_Found = 0x0A,
        Attribute_Not_Long = 0x0B,
        Insufficient_Encryption_Key_Size = 0x0C,
        Invalid_Attribute_Value_Length = 0x0D,
        Unlikely_Error = 0x0E,
        Insufficient_Encryption = 0x0F,
        Unsupported_Group_Type = 0x10,
        Insufficient_Resource = 0x11,
        Database_Out_Of_Sync = 0x12,
        Value_Not_Allowed = 0x13,
        Write_Request_Rejected = 0xFC,
        Client_Characteristic_Configuration_Descriptor_Improperly_Configured = 0xFD,
        Procedure_Already_in_Progress = 0xFE,
        Out_of_Range = 0xFF
    }

    public static class Utilities
    {
        public static ushort ConvertUuidToShortId(Guid uuid)
        {
            // Get the short Uuid
            var bytes = uuid.ToByteArray();
            var shortUuid = (ushort)(bytes[0] | (bytes[1] << 8));
            return shortUuid;
        }

        public static byte[] ReadBufferToBytes(IBuffer buffer)
        {
            var dataLength = buffer.Length;
            var data = new byte[dataLength];
            using (var reader = DataReader.FromBuffer(buffer))
            {
                reader.ReadBytes(data);
            }
            return data;
        }

        public static string FormatValue(IBuffer buffer, DataFormat format)
        {
            byte[] data;
            CryptographicBuffer.CopyToByteArray(buffer, out data);

            switch (format)
            {
                case DataFormat.ASCII:
                    return Encoding.ASCII.GetString(data);

                case DataFormat.UTF8:
                    return Encoding.UTF8.GetString(data);

                case DataFormat.Dec:
                    return string.Join(" ", data.Select(b => b.ToString("00")));

                case DataFormat.Hex:
                    return BitConverter.ToString(data).Replace("-", " ");

                case DataFormat.Bin:
                    var s = string.Empty;
                    foreach (var b in data) s += Convert.ToString(b, 2).PadLeft(8, '0') + " ";
                    return s;

                default:
                    return Encoding.ASCII.GetString(data);
            }
        }

        public static string FormatValueMultipleFormattes(IBuffer buffer, List<DataFormat> formatList)
        {
            byte[] data;
            String stringBuffer = "";

            CryptographicBuffer.CopyToByteArray(buffer, out data);
            for (int dataFormatIdx = 0; dataFormatIdx < formatList.Count; dataFormatIdx++)
            {
                DataFormat dataFormat = formatList[dataFormatIdx];
                switch (dataFormat)
                {
                    case DataFormat.ASCII:
                        stringBuffer += $"ascii: {Encoding.ASCII.GetString(data)}";
                        break;

                    case DataFormat.UTF8:
                        stringBuffer += $"utf8:\t{Encoding.UTF8.GetString(data)}";
                        break;

                    case DataFormat.Dec:
                        stringBuffer += $"dec:\t{string.Join(" ", data.Select(b => b.ToString("00")))}";
                        break;

                    case DataFormat.Hex:
                        stringBuffer += $"hex:\t{BitConverter.ToString(data).Replace("-", " ")}";
                        break;

                    case DataFormat.Bin:
                        var s = string.Empty;
                        foreach (var b in data) s += Convert.ToString(b, 2).PadLeft(8, '0') + " ";
                        stringBuffer += $"bin:\t{s}";
                        break;

                    default:
                        stringBuffer += $"ascii: {Encoding.ASCII.GetString(data)}";
                        break;
                }
                if (dataFormatIdx != formatList.Count - 1)
                {
                    stringBuffer += "\n";
                }
            }
            return stringBuffer;
        }
        public static IBuffer FormatData(string data, DataFormat format)
        {
            try
            {
                // For text formats, use CryptographicBuffer
                if (format == DataFormat.ASCII || format == DataFormat.UTF8)
                {

                    IBuffer result_data = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
                    using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(result_data))
                    {
                        dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                        //Console.WriteLine("result: {0}", dataReader.ReadString(result_data.Length) );
                    }
                    return result_data;
                }
                else
                {
                    string[] values = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    byte[] bytes = new byte[values.Length];

                    for (int i = 0; i < values.Length; i++)
                        bytes[i] = Convert.ToByte(values[i], (format == DataFormat.Dec ? 10 : (format == DataFormat.Hex ? 16 : 2)));

                    var writer = new DataWriter();
                    writer.ByteOrder = ByteOrder.LittleEndian;
                    writer.WriteBytes(bytes);

                    return writer.DetachBuffer();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }
        public static string GetIdByNameOrNumber(object collection, string name)
        {
            string result = string.Empty;

            // If number is specified, try to open BLE device by specific number
            if (name[0] == '#')
            {
                int devNumber = -1;
                if (int.TryParse(name.Substring(1), out devNumber))
                {
                    // Try to find device ID by number
                    if (collection is List<DeviceInformation>)
                    {
                        if (0 <= devNumber && devNumber < (collection as List<DeviceInformation>).Count)
                        {
                            result = (collection as List<DeviceInformation>)[devNumber].Id;
                        }
                        else
                            if (Console.IsOutputRedirected)
                            Console.WriteLine("Device number {0:00} is not in device list range", devNumber);
                    }
                    // for services or attributes
                    else
                    {
                        if (0 <= devNumber && devNumber < (collection as List<BluetoothLEAttributeDisplay>).Count)
                        {
                            result = (collection as List<BluetoothLEAttributeDisplay>)[devNumber].Name;
                        }
                    }
                }
                else
                    if (!Console.IsOutputRedirected)
                    Console.WriteLine("Invalid device number {0}", name.Substring(1));
            }
            // else try to find name
            else
            {
                // ... for devices
                if (collection is List<DeviceInformation>)
                {
                    var foundDevices = (collection as List<DeviceInformation>).Where(d => d.Name.ToLower().StartsWith(name.ToLower())).ToList();
                    if (foundDevices.Count == 0)
                    {
                        if (!Console.IsOutputRedirected)
                            Console.WriteLine("Can't connect to {0}.", name);
                    }
                    else if (foundDevices.Count == 1)
                    {
                        result = foundDevices.First().Id;
                    }
                    else
                    {
                        if (!Console.IsOutputRedirected)
                            Console.WriteLine("Found multiple devices with names started from {0}. Please provide an exact name.", name);
                    }
                }
                // for services or attributes
                else
                {
                    var foundDispAttrs = (collection as List<BluetoothLEAttributeDisplay>).Where(d => d.Name.ToLower().StartsWith(name.ToLower())).ToList();
                    if (foundDispAttrs.Count == 0)
                    {
                        if (Console.IsOutputRedirected)
                            Console.WriteLine("No service/characteristic found by name {0}.", name);
                    }
                    else if (foundDispAttrs.Count == 1)
                    {
                        result = foundDispAttrs.First().Name;
                    }
                    else
                    {
                        if (Console.IsOutputRedirected)
                            Console.WriteLine("Found multiple services/characteristic with names started from {0}. Please provide an exact name.", name);
                    }
                }
            }
            return result;
        }
        public static string FormatProtocolError(byte? protocolError)
        {
            if (protocolError == null)
            {
                return "";
            }
            string protocolErrorCodeName = Enum.GetName(typeof(ProtocolErrorCode), protocolError);
            if (protocolErrorCodeName != null)
            {
                protocolErrorCodeName = protocolErrorCodeName.Replace("_", " ");
                return String.Format("0x{0:X2}: {1}", protocolError, protocolErrorCodeName);
            }
            return String.Format("0x{0:X2}: Unknown", protocolError);
        }
    }

    public static class TaskExtensions
    {
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
        }
    }

}
