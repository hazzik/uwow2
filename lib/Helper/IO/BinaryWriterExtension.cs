using System.IO;
using System.Text;

namespace Hazzik.IO
{
    public static class BinaryWriterExtension
    {
        public static void WritePascalString(this BinaryWriter self, string value)
        {
            WritePascalString(self, value, Encoding.UTF8);
        }

        public static void WritePascalString(this BinaryWriter self, string value, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(value ?? string.Empty);
            self.Write(bytes.Length);
            self.Write(bytes);
        }

        public static void WriteCString(this BinaryWriter self, string value)
        {
            WriteCString(self, value, Encoding.UTF8);
        }

        public static void WriteCString(this BinaryWriter self, string value, Encoding encoding)
        {
            self.Write(encoding.GetBytes(value ?? string.Empty));
            self.Write((byte) 0);
        }

        public static void WritePackGuid(this BinaryWriter self, ulong guid)
        {
            var buff = new byte[8];
            var mask = (byte) 0;
            int offset = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((byte) guid != 0)
                {
                    buff[offset++] = (byte) guid;
                    mask |= (byte) (1 << i);
                }
                guid >>= 8;
            }
            self.Write(mask);
            self.Write(buff, 0, offset);
        }
    }
}