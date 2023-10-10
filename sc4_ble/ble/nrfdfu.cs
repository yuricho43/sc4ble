// Console.WriteLine
using System;
// List
using System.Collections.Generic;

// for AsBuffer();
using System.Runtime.InteropServices.WindowsRuntime;
// for IBuffer
using Windows.Storage.Streams;
// for Marshal
using System.Runtime.InteropServices;
// for await Task.delay
using System.Threading.Tasks;
// for FileStream, BinaryReader
using System.IO;

// for crc32()
using System.IO.Hashing;

using Ble.Service;

namespace nrf.DFU
{
	

	static class Constants
	{
		public const string DFU_SUBSCRIBTION_INIT = "INIT";
		//public const string DFU_SERVICE_UUID	= "0000fe59-0000-1000-8000-00805f9b34fb";
		public const string DFU_SERVICE_UUID	= "65113";
		public const string DFU_CONTROL_UUID	= "8EC90001-F315-4F60-9FB8-838830DAEA50";
		public const string DFU_DATA_UUID		= "8EC90002-F315-4F60-9FB8-838830DAEA50";
		public const string DFU_BUTTONLESS_UUID	= "8EC90003-F315-4F60-9FB8-838830DAEA50";
	}

	public enum nrf_dfu_op: byte
	{
		NRF_DFU_OP_PROTOCOL_VERSION = 0x00,  //!< Retrieve protocol version.
		NRF_DFU_OP_OBJECT_CREATE = 0x01,     //!< Create selected object.
		NRF_DFU_OP_RECEIPT_NOTIF_SET = 0x02, //!< Set receipt notification.
		NRF_DFU_OP_CRC_GET = 0x03,           //!< Request CRC of selected object.
		NRF_DFU_OP_OBJECT_EXECUTE = 0x04,    //!< Execute selected object.
		NRF_DFU_OP_OBJECT_SELECT = 0x06,     //!< Select object.
		NRF_DFU_OP_MTU_GET = 0x07,           //!< Retrieve MTU size.
		NRF_DFU_OP_OBJECT_WRITE = 0x08,      //!< Write selected object.
		NRF_DFU_OP_PING = 0x09,              //!< Ping.
		NRF_DFU_OP_HARDWARE_VERSION = 0x0A,  //!< Retrieve hardware version.
		NRF_DFU_OP_FIRMWARE_VERSION = 0x0B,  //!< Retrieve firmware version.
		NRF_DFU_OP_ABORT = 0x0C,             //!< Abort the DFU procedure.
		NRF_DFU_OP_RESPONSE = 0x60,          //!< Response.
		NRF_DFU_OP_INVALID = 0xFF,
	}

	/**
 * @brief DFU operation result code.
 */
	public enum nrf_dfu_result: byte 
	{
		NRF_DFU_RES_CODE_INVALID = 0x00,                 //!< Invalid opcode.
		NRF_DFU_RES_CODE_SUCCESS = 0x01,                 //!< Operation successful.
		NRF_DFU_RES_CODE_OP_CODE_NOT_SUPPORTED = 0x02,   //!< Opcode not supported.
		NRF_DFU_RES_CODE_INVALID_PARAMETER = 0x03,       //!< Missing or invalid parameter value.
		NRF_DFU_RES_CODE_INSUFFICIENT_RESOURCES = 0x04,  //!< Not enough memory for the data object.
		NRF_DFU_RES_CODE_INVALID_OBJECT = 0x05,          //!< Data object does not match the firmware and hardware requirements, the signature is wrong, or parsing the command failed.
		NRF_DFU_RES_CODE_UNSUPPORTED_TYPE = 0x07,        //!< Not a valid object type for a Create request.
		NRF_DFU_RES_CODE_OPERATION_NOT_PERMITTED = 0x08, //!< The state of the DFU process does not allow this operation.
		NRF_DFU_RES_CODE_OPERATION_FAILED = 0x0A,        //!< Operation failed.
		NRF_DFU_RES_CODE_EXT_ERROR = 0x0B,               //!< Extended error. The next byte of the response contains the error code of the extended error (see @ref nrf_dfu_ext_error_code_t.
	}
	public enum nrf_dfu_firmware_type: byte
	{
		NRF_DFU_FIRMWARE_TYPE_SOFTDEVICE = 0x00,
		NRF_DFU_FIRMWARE_TYPE_APPLICATION = 0x01,
		NRF_DFU_FIRMWARE_TYPE_BOOTLOADER = 0x02,
		NRF_DFU_FIRMWARE_TYPE_UNKNOWN = 0xFF,
	}

	public enum NRF_ERROR_CODE : int
	{
		// ERROR_NONE
		NRF_ERROR_NONE,
		NRF_CMD_WRONG_PARAMETER,
		// INFORMATION ENUM
		NRF_FOUND_DEVICE,
		NRF_CANNOT_FOUND_DEVICE,
		NRF_CONNECTED,
		NRF_NO_CONNECTED,

		// ERROR ENUM
		NRF_ERROR_SERVICE_SET,
		NRF_ERROR_SUBSCRIBTOIN_SET,
		NRF_ERROR_SUBSCRIBTOIN_DATA,

		NRF_ERROR_DFU_OBJ_WRITE_FILE,
		NRF_ERROR_DFU_CRC_CMP,
		NRF_ERROR_DFU_OBJ_EXECUTE,
		NRF_ERROR_DFU_OBJ_CREATE,
		NRF_ERROR_DFU_OBJ_SELECT,
		NRF_ERROR_DFU_GET_CRC,

		NRF_DFU_RET_FW_VERSION,


		NRF_UNKNOWN_ERROR,
	}

	public struct subscribtion_result
	{
		public string byte_string;
		public byte[] byte_array;
	}

	public class nrfDFU
	{

		Bleservice ble;
		subscribtion_result sub_result;
		string ble_devname;
		ulong dfu_max_size = 0;
		ulong dfu_mtu = 20;
		ulong dfu_current_crc = 0;
		Crc32 dfu_crc32;
		string dfu_file_name;
		BinaryReader dfu_br = null;
		int dfu_file_size = 0;
 		byte[] dfu_buffer;

		public nrfDFU()
		{
			Console.WriteLine("instance create");
		}

		public async Task<NRF_ERROR_CODE> Open(string devName)
		{
			NRF_ERROR_CODE nrf_result = NRF_ERROR_CODE.NRF_UNKNOWN_ERROR;
			ERROR_CODE ble_result = ERROR_CODE.NONE;
			ble = new Bleservice();
			ble_result = ble.StartScan(devName, (d) => { });
			if (!ble_result.Equals(ERROR_CODE.BLE_FOUND_DEVICE))
			{
				Console.WriteLine($"Scan Failed: {devName}");
				return NRF_ERROR_CODE.NRF_CANNOT_FOUND_DEVICE;
			}
			ble_result = await ble.OpenDevice(devName);
			if (!ble_result.Equals(ERROR_CODE.NONE))
			{
				Console.WriteLine($"OpenDevice Failed: {devName}");
				return NRF_ERROR_CODE.NRF_NO_CONNECTED;
			}
			ble_result = await ble.SetService(Constants.DFU_SERVICE_UUID);
			if (!ble_result.Equals(ERROR_CODE.NONE))
			{
				Console.WriteLine($"Set Service Failed: {Constants.DFU_SERVICE_UUID}");
				return NRF_ERROR_CODE.NRF_ERROR_SERVICE_SET;
			}
			ble_devname = devName;
			nrf_result = NRF_ERROR_CODE.NRF_FOUND_DEVICE;
			return nrf_result;
		}
		public async Task<NRF_ERROR_CODE> ControlSubscribtion()
		{
			NRF_ERROR_CODE nrf_result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			string service_subchar = Constants.DFU_SERVICE_UUID + "/" + Constants.DFU_CONTROL_UUID;
			//string service_subchar =  Constants.DFU_CONTROL_UUID;
			string result_string = await ble.SubscribeCharacteristic(ble_devname, service_subchar, (arg) =>
			{
				sub_result.byte_string = arg;

				var parts = arg.Split('\t');
				if (parts.Length < 2)
				{
					sub_result.byte_string = GetErrorString(NRF_ERROR_CODE.NRF_ERROR_SUBSCRIBTOIN_DATA);
					Console.WriteLine($"Callback susbscribtion data error: {sub_result.byte_string}");
					return;
				}
				string[] values = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				sub_result.byte_array = new byte[values.Length];

				for (int i = 0; i < values.Length; i++)
					sub_result.byte_array[i] = Convert.ToByte(values[i], 16);
				Console.WriteLine($"susbscribtion data: {sub_result.byte_string}");
			});
			if (!result_string.Equals(ble.GetErrorString(ERROR_CODE.NONE)))
			{
				nrf_result = NRF_ERROR_CODE.NRF_UNKNOWN_ERROR;
			}
			return nrf_result;
		}

		public async Task<NRF_ERROR_CODE> BleWrite(string CharUuid, string stringData)
		{
			NRF_ERROR_CODE nrf_result = NRF_ERROR_CODE.NRF_ERROR_NONE;

			//string service_subchar = Constants.DFU_SERVICE_UUID + "/" + CharUuid + " "+ stringData;
			string service_subchar = CharUuid + " " + stringData;
			//Console.WriteLine($"BleWrite: {service_subchar}");
			var result = await ble.WriteCharacteristic(ble_devname, service_subchar);
			if (!result.Equals(ble.GetErrorString(ERROR_CODE.NONE)))
			{
				nrf_result = NRF_ERROR_CODE.NRF_UNKNOWN_ERROR;
			}
			return nrf_result;
		}
		public async Task<NRF_ERROR_CODE> ControlWrite(byte[] data)
		{
			var stringData = $"{BitConverter.ToString(data).Replace("-", " ")}";
			Console.WriteLine($"ControlWrite: {stringData}");
			sub_result.byte_string = Constants.DFU_SUBSCRIBTION_INIT;
			return await BleWrite(Constants.DFU_CONTROL_UUID, stringData);
		}
		public async Task<NRF_ERROR_CODE> DataWrite()
		{
			// sub_result.byte_string = Constants.DFU_SUBSCRIBTION_INIT;
			//Console.WriteLine($"DataWrite");
			// await BleWrite(Constants.DFU_DATA_UUID, stringData);
			ble.write_set_char(ble_devname, Constants.DFU_DATA_UUID);
			ble.write_set_data(ref dfu_buffer);
			await ble.write_run();
			return NRF_ERROR_CODE.NRF_ERROR_NONE;
		}

		[StructLayout(LayoutKind.Explicit, Pack = 1)]
		class object_create
		{
			[FieldOffset(0)]
			nrf_dfu_op request;
			[FieldOffset(1)]
			byte type;
			[FieldOffset(2)]
			ulong size;
		}

		public struct object_select
		{
			byte request;
			byte type;
		}

		public struct crc_request
		{
			byte request;
		}

		public byte[] getBytes<T>(T str)
		{
			int size = Marshal.SizeOf(str);
			byte[] arr = new byte[size];

			IntPtr ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(str, ptr, true);
			Marshal.Copy(ptr, arr, 0, size);
			Marshal.FreeHGlobal(ptr);
			return arr;
		}
		public string GetErrorString(NRF_ERROR_CODE param)
		{
			return "NRF_ERROR_CODE." + param.ToString();
		}

#if false
		public ulong  leto32host(byte byte_0, byte byte_1, byte byte_2, byte byte_3)
		{
			ulong  result = (ulong)(byte_3 << 24) ||
							(ulong)(byte_2 << 16) ||
							(ulong)(byte_1 << 8) ||
							(ulong)(byte_0);
			Console.WriteLine($"leto32host: result = {result}, source {byte_0} {byte_1} {byte_2} {byte_3}");
			return result;
		}
#endif

		private bool HasGotResponse()
		{
			try
			{
				if (sub_result.byte_string.Equals(Constants.DFU_SUBSCRIBTION_INIT))
				{
					return false;
				}
				else
				{
					return true;
				}

			}
			catch
			{
				return false; // Excel will throw an exception, meaning its busy
			}
		}

		public async Task<bool> wait_notification()
		{
			bool wait_result = true;
			int i = 100;
			while (!HasGotResponse())
			{

				Console.WriteLine("No Response yet");
				if ((--i) == 0)
				{
					wait_result = false;
					break;
				}
				await Task.Delay(10);
			}
			return wait_result;
		}
		public async Task<bool> dfu_set_packet_receive_notification(ushort prn)
		{
			NRF_ERROR_CODE result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			Console.WriteLine($"Set packet receive notification : {prn}");

			byte op_cmd = (byte)nrf_dfu_op.NRF_DFU_OP_RECEIPT_NOTIF_SET;
			result = await ControlSubscribtion();
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"ControlSubscribtion error: {result}");
				return false;
			}
			byte[] writedata = { op_cmd, 0, 0 };
			writedata[1] = (byte)((byte)prn & 0xff);
			writedata[2] = (byte)((byte)(prn >> 8) & 0xff);
			result = await ControlWrite(writedata);
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"ControlWrite:  {result}");
				return false;
			}
			bool wait_noti = await wait_notification();
			Console.WriteLine($"wait_notification result:  {wait_noti}");
			if (wait_noti)
			{
				Console.WriteLine($"noti result.string : {sub_result.byte_string}");
			}
			return wait_noti;
		}

		public async Task<ulong> dfu_get_crc()
		{
			NRF_ERROR_CODE result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			Console.WriteLine("dfu_object_execute");
			byte op_cmd = (byte)nrf_dfu_op.NRF_DFU_OP_CRC_GET;
			byte[] writedata = { op_cmd };

			result = await ControlWrite(writedata);
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"dfu_get_crc: ControlWrite:  {result}");
				return 0;
			}
			bool result_noti = await wait_notification();
			if (!result_noti)
			{
				Console.WriteLine($"dfu_get_crc: wait_notification result:  {result_noti}");
				return 0;
			}
			if (sub_result.byte_array.Length < 3)
			{
				Console.WriteLine($"dfu_get_crc: wait_notification error : result length :  {sub_result.byte_array.Length}");
				return 0;
			}
			if (sub_result.byte_array[0] != (byte)nrf_dfu_op.NRF_DFU_OP_RESPONSE ||
				sub_result.byte_array[1] != op_cmd ||
				sub_result.byte_array[2] != (byte)nrf_dfu_result.NRF_DFU_RES_CODE_SUCCESS)
			{

				Console.WriteLine($"dfu_get_crc: response result error: {sub_result.byte_array[0]}, {sub_result.byte_array[1]} , {sub_result.byte_array[2]}");
				return 0;
			}
			ulong result_offset = BitConverter.ToUInt32(sub_result.byte_array, 3 /* Which byte position to convert */);
			ulong result_crc = BitConverter.ToUInt32(sub_result.byte_array, 7 /* Which byte position to convert */);
			Console.WriteLine($"dfu_get_crc: offset {result_offset}, crc 0x{result_crc:X}");
			return result_crc;
		}
		public async Task<bool> dfu_object_create(byte type, ulong size)
		{

			NRF_ERROR_CODE result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			Console.WriteLine($"dfu_object_create: type: {type}, size: {size}");

			byte op_cmd = (byte)nrf_dfu_op.NRF_DFU_OP_OBJECT_CREATE;
			byte[] writedata = { op_cmd, type, 0x00, 0x00, 0x00, 0x00 };
			writedata[2] = (byte)((byte)size & 0xff);
			writedata[3] = (byte)((byte)(size >> 8) & 0xff);
			writedata[4] = (byte)((byte)(size >> 16) & 0xff);
			writedata[5] = (byte)((byte)(size >> 24) & 0xff);
			result = await ControlWrite(writedata);
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"dfu_object_create: ControlWrite:  {result}");
				return false;
			}
			bool result_noti = await wait_notification();
			if (!result_noti)
			{
				Console.WriteLine($"dfu_object_create: wait_notification result:  {result_noti}");
				return false;
			}
			if (sub_result.byte_array.Length < 3)
			{
				Console.WriteLine($"dfu_object_create: wait_notification error : result length :  {sub_result.byte_array.Length}");
				return false;
			}
			if (sub_result.byte_array[0] != (byte)nrf_dfu_op.NRF_DFU_OP_RESPONSE ||
				sub_result.byte_array[1] != op_cmd ||
				sub_result.byte_array[2] != (byte)nrf_dfu_result.NRF_DFU_RES_CODE_SUCCESS)
			{

				Console.WriteLine($"dfu_object_create: response result error: {sub_result.byte_array[0]:X}, {sub_result.byte_array[1]:X} , {sub_result.byte_array[2]:X}");
				return false;
			}
			Console.WriteLine($"dfu_object_create: success");
			return true;
		}

		public async Task<(bool flag, ulong offset, ulong crc)> dfu_object_select(byte type)
		{
			NRF_ERROR_CODE result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			Console.WriteLine($"dfu_object_select: type {type}");
			byte op_cmd = (byte)nrf_dfu_op.NRF_DFU_OP_OBJECT_SELECT;
			byte[] writedata = { op_cmd, type };

			result = await ControlWrite(writedata);
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"dfu_object_select: ControlWrite:  {result}");
				return (false, 0, 0);
			}
			bool result_noti = await wait_notification();
			if (!result_noti)
			{
				Console.WriteLine($"dfu_object_select: wait_notification result:  {result_noti}");
				return (false, 0, 0);
			}

			if (sub_result.byte_array.Length < 3)
			{
				Console.WriteLine($"dfu_object_select: wait_notification error : result length :  {sub_result.byte_array.Length}");
				return (false, 0, 0);
			}
			if (sub_result.byte_array[0] != (byte)nrf_dfu_op.NRF_DFU_OP_RESPONSE ||
				sub_result.byte_array[1] != op_cmd ||
				sub_result.byte_array[2] != (byte)nrf_dfu_result.NRF_DFU_RES_CODE_SUCCESS)
			{

				Console.WriteLine($"dfu_object_select: response result error: {sub_result.byte_array[0]}, {sub_result.byte_array[0]} , {sub_result.byte_array[0]}");
				return (false, 0, 0);
			}

			dfu_max_size = BitConverter.ToUInt32(sub_result.byte_array, 3 /* Which byte position to convert */);
			ulong offset = BitConverter.ToUInt32(sub_result.byte_array, 7 /* Which byte position to convert */);
			ulong crc = BitConverter.ToUInt32(sub_result.byte_array, 11 /* Which byte position to convert */);


			//dfu_max_mtu_size = leto32host(sub_result.byte_array[3], sub_result.byte_array[4], sub_result.byte_array[5], sub_result.byte_array[6]);
			//ulong offset = leto32host(sub_result.byte_array[7], sub_result.byte_array[8], sub_result.byte_array[9], sub_result.byte_array[10]);
			//ulong crc	 = leto32host(sub_result.byte_array[11], sub_result.byte_array[12], sub_result.byte_array[13], sub_result.byte_array[14]);

			Console.WriteLine($"dfu_object_select: dfu_max_mtu_size {dfu_max_size}, offset: {offset}, crc: {crc}");
			return (true, offset, crc);
		}
		public async Task<NRF_ERROR_CODE> dfu_object_execute()
		{
			NRF_ERROR_CODE result = NRF_ERROR_CODE.NRF_ERROR_NONE;
			Console.WriteLine("dfu_object_execute");
			byte op_cmd = (byte)nrf_dfu_op.NRF_DFU_OP_OBJECT_EXECUTE;
			byte[] writedata = { op_cmd };

			result = await ControlWrite(writedata);
			if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
			{
				Console.WriteLine($"dfu_object_execute: ControlWrite:  {result}");
				return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_EXECUTE;
			}
			bool result_noti = await wait_notification();
			if (!result_noti)
			{
				Console.WriteLine($"dfu_object_execute: wait_notification result:  {result_noti}");
				return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_EXECUTE;
			}
			if (sub_result.byte_array.Length < 3)
			{
				Console.WriteLine($"dfu_object_execute: wait_notification error : result length :  {sub_result.byte_array.Length}");
				return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_EXECUTE;
			}
			if (sub_result.byte_array[0] != (byte)nrf_dfu_op.NRF_DFU_OP_RESPONSE ||
				sub_result.byte_array[1] != op_cmd ||
				sub_result.byte_array[2] != (byte)nrf_dfu_result.NRF_DFU_RES_CODE_SUCCESS)
			{

				Console.WriteLine($"dfu_object_execute: response result error: {sub_result.byte_array[0]}, {sub_result.byte_array[1]} , {sub_result.byte_array[2]}");
				return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_EXECUTE;
			}
			Console.WriteLine($"dfu_object_execute: success");
			return NRF_ERROR_CODE.NRF_ERROR_NONE;
		}


		public async Task<bool> dfu_object_write_file(int size)
		{
			int written = 0;
			int length;
			int to_read;

			//Console.WriteLine($"Write data file:{dfu_file_name}, Size {size}, MTU {dfu_mtu}");

			do
			{
				to_read = Math.Min((int)dfu_mtu, size - written);
				dfu_buffer = dfu_br.ReadBytes((int)to_read);
				length = dfu_buffer.Length;
				if (length == 0)
				{ // EOF
					Console.WriteLine("dfu_object_write_file: Write Done, write size :{written} ");
					break;
				}
				var result = await DataWrite();
				if (!result.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
				{
					Console.WriteLine($"DataWrite error: {result.ToString()}");
					return false;
				}
				written += length;
				dfu_crc32.Append(dfu_buffer);
				//Console.WriteLine($"dfu_object_write_file: size:{size} written:{written}");
			} while ((length > 0) && (written < size) && (written < (int)dfu_max_size));
			var checkSum = dfu_crc32.GetCurrentHash();
			dfu_current_crc = BitConverter.ToUInt32(checkSum, 0 /* Which byte position to convert */);

			//Console.WriteLine($"dfu_object_write_file: dfu_max_size:{dfu_max_size}, size:{size } written:{written} bytes CRC: 0x{dfu_current_crc:X8}");

			return true;
		}

		private static readonly int CHUNK_SIZE = 200;
		public ulong calc_crc_file(string filename, int size)
		{
			byte[] fbuf = new byte[CHUNK_SIZE];
			int read = 0;
			int to_read;

			int length = 0;
			var crc_file = new Crc32();

			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			BinaryReader br = new BinaryReader(fs);
			int file_size = (int)br.BaseStream.Length;

			do
			{

				to_read = Math.Min(CHUNK_SIZE, size - read);
				var chunk = br.ReadBytes(to_read);
				length = chunk.Length;
				if (length == 0)
				{
					Console.WriteLine("calc_crc_file: Read Done, read size :{read} ");
					break;
				}
				read += length;
				crc_file.Append(chunk);
			} while (length > 0 && read < size);

			var checkSum = crc_file.GetCurrentHash();
			var hash = BitConverter.ToString(checkSum).Replace("-", "").ToLower();

			Console.WriteLine("crc result {hash}");

			return 0;
		}

		/** return: failed, success, fw_version too low */
		public async Task<NRF_ERROR_CODE> dfu_object_write_procedure(byte type, string filename)
		{
			NRF_ERROR_CODE ret;
			dfu_crc32 = new Crc32();
			dfu_file_name = filename;
			FileStream fs = new FileStream(dfu_file_name, FileMode.Open, FileAccess.Read);
			dfu_br = new BinaryReader(fs);
			dfu_file_size = (int)dfu_br.BaseStream.Length;

			var result = await dfu_object_select(type);
			if (!result.flag)
			{
				return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_SELECT;
			}

			/* object with same length and CRC already received */
			if (((int)result.offset == dfu_file_size) && (calc_crc_file(dfu_file_name, dfu_file_size) == result.crc))
			{
				Console.WriteLine("Object already received: type: ${type} filename: ${dfu_file_name} ");
				/* Don't transfer anything and skip to the Execute command */
				return await dfu_object_execute();
			}
			/* parts already received */
			if (result.offset > 0)
			{
				ulong remain = result.offset % dfu_max_size;
				Console.WriteLine("Object partially received (offset {0} remaining {1}", result.offset, remain);

				dfu_current_crc = calc_crc_file(dfu_file_name, (int)result.offset);
				if (result.crc != dfu_current_crc)
				{
					/* invalid crc, remove corrupted data, rewind and
					* create new object below */
					result.offset -= remain > 0 ? remain : dfu_max_size;
					Console.WriteLine("CRC does not match (restarting from {0})", result.offset);
					dfu_current_crc = calc_crc_file(dfu_file_name, (int)result.offset);
				}
			}
			else if (result.offset == 0)
			{
				//				dfu_current_crc = crc32(0L, Z_NULL, 0);
				dfu_current_crc = 0;
			}
			/* create and write objects of max_size */
			Console.WriteLine($"dfu_object_write_procedure: type: {type}, name: {filename}, size: {dfu_file_size}");
			for (int i = 0; i < dfu_file_size; i += (int)dfu_max_size)
			{
				int osz = Math.Min(dfu_file_size - i, (int)dfu_max_size);
				var obj_result = await dfu_object_create(type, (ulong)osz);
				if (!obj_result)
				{
					Console.WriteLine($"dfu_object_write_procedure: dfu_object_create Error: {obj_result}");
					return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_CREATE;
				}
				var write_result = await dfu_object_write_file(osz);
				if (!write_result)
				{
					Console.WriteLine($"dfu_object_write_procedure: dfu_object_write Error");
					return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_WRITE_FILE;
				}

				ulong rcrc = await dfu_get_crc();
				if (rcrc != dfu_current_crc)
				{
					Console.WriteLine($"dfu_object_write_procedure: size {osz}, CRC failed, received crc {rcrc:X} vs calculated crc {dfu_current_crc:X}");
					return NRF_ERROR_CODE.NRF_ERROR_DFU_CRC_CMP;
				}

				ret = await dfu_object_execute();
				if (!ret.Equals(NRF_ERROR_CODE.NRF_ERROR_NONE))
				{
					Console.WriteLine($"dfu_object_write_procedure: {ret}");
					return NRF_ERROR_CODE.NRF_ERROR_DFU_OBJ_EXECUTE;
				}
				Console.WriteLine($"dfu_object_write_procedure: offfset {i}");
			}
			Console.WriteLine($"dfu_object_write_procedure: Done");
			return NRF_ERROR_CODE.NRF_ERROR_NONE;
		}  // end of the function

		public ulong dfu_object_write_procedure_progress()
		{
			ulong position = 0;
			if ( (dfu_br != null) && (dfu_file_size != 0 ) )
			{
				position = (ulong)(((float)dfu_br.BaseStream.Position*100)/dfu_file_size);
			}
			return position;
		}
	}
}