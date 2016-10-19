using System;
using System.Linq;
using System.Text;

namespace MHSEC_G
{
    public class Model
    {
        public const uint SAVE_FILE_SIZE = 483976;

        private readonly byte[] _save_file;

        public byte[] save_file
        {
            get { return _save_file; }
        }

        public Model(byte[] save_file)
        {
            if (save_file.Length != SAVE_FILE_SIZE)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_INVALID_FILE_SIZE, "Invalid file size.\nExpected: " + SAVE_FILE_SIZE + " Got: " + save_file.Length);
            }
            _save_file = save_file;
        }

        public static uint byte_to_uint(byte b)
        {
            return (uint) (b) & 0xFF;
        }

        public static void write_byte(byte[]arr, uint offset, uint val)
        {
            if(offset < arr.Length)
                arr[offset] = (byte)(val&0xFF);
            else
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_WRITE_BYTE_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }
        }

        public static uint byte_to_uint16_le(byte[] arr, uint offset)
        {
            if (arr.Length < offset + 2)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_READ_UINT16_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }
            return byte_to_uint(arr[offset]) | (byte_to_uint(arr[offset + 1]) << 8);
        }

        public static void write_uint16_le(byte[] arr, uint offset, uint val)
        {
            if (arr.Length < offset + 2)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_WRITE_UINT16_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }
            arr[offset] = (byte) (val & 0xFF);
            arr[offset + 1] = (byte) ((val >> 8) & 0xFF);
        }

        public static uint byte_to_uint32_le(byte[] arr, uint offset)
        {
            if (arr.Length < offset + 4)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_READ_UINT32_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }
            return byte_to_uint(arr[offset]) | (byte_to_uint(arr[offset + 1]) << 8) |
                   (byte_to_uint(arr[offset + 2]) << 16) | (byte_to_uint(arr[offset + 3]) << 24);
        }

        public static void write_uint32_le(byte[] arr, uint offset, uint val)
        {
            if (arr.Length < offset + 4)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_WRITE_UINT32_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }
            arr[offset] = (byte) (val & 0xFF);
            arr[offset + 1] = (byte) ((val >> 8) & 0xFF);
            arr[offset + 2] = (byte) ((val >> 16) & 0xFF);
            arr[offset + 3] = (byte) ((val >> 24) & 0xFF);
        }

        public static string read_unicode_string(byte[] arr, uint offset, uint max_char)
        {
            StringBuilder name = new StringBuilder();
            uint read_char = 0;
            for (uint i = offset; i < arr.Length; i += 2)
            {
                if (read_char < max_char)
                {
                    uint each_char = byte_to_uint16_le(arr, i);
                    if (each_char == 0)
                        break;
                    name.Append(Convert.ToChar(each_char));
                    read_char++;
                }
                else
                {
                    break;
                }
            }
            return name.ToString();
        }

        public static void write_unicode_string(byte[] arr, uint offset, string str, uint length)
        {
            if (length < str.Length || arr.Length < offset + length)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_WRITE_UNICODE_OVERFLOW, "Buffer overflowed.\nBound " + arr.Length + " Offset " + offset);
            }

            Array.Clear(arr, (int) offset, (int)length*2);
            for (uint i = 0; i < str.Length; i ++)
            {
                write_uint16_le(arr, offset + i*2, str.ElementAt((int) i));
            }
            return;
        }


        public static bool parse_hex_string(string val, out uint result)
        {
            result = 0;
            try
            {
                result = Convert.ToUInt32(val, 16);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
            return true;
        }
    }
}
