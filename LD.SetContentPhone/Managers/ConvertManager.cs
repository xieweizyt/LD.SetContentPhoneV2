using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public class ConvertManager
    {
        public static string StringToUCS2(string message)
        {
            StringBuilder ucs2Message = new StringBuilder();

            foreach (char c in message)
            {
                byte[] ucs2Bytes = Encoding.BigEndianUnicode.GetBytes(c.ToString());

                // UCS2 是大端序，所以我们需要将字节数组逆序
                Array.Reverse(ucs2Bytes);
                ucs2Message.Append(BitConverter.ToString(ucs2Bytes).Replace("-", ""));
            }

            return ucs2Message.ToString();
        }

        // 将字符串转为 UCS2 (Unicode Big Endian -> 十六进制)
        public static string ToUCS2(string input)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(input);
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public static byte[] ToUCS2Bytes(string text)
        {
            return Encoding.BigEndianUnicode.GetBytes(text);
        }

        public static string ToUCS2Hex(string s)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(s);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static string FromUCS2Hex(string hex)
        {
            // 将十六进制字符串转换为字节数组
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            // 将字节数组转换为 UCS2 编码的字符串
            return Encoding.BigEndianUnicode.GetString(bytes);
        }
    }
}
