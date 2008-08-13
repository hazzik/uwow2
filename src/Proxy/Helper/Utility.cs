using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace UWoW.Helper
{
	public class Utility
	{
		#region Imported members

		[DllImport("wfk2.dll", EntryPoint = "FindKey")]
		public static extern bool ExtractKey(string name, byte[] key);

		#endregion

		public static string ReadSz(Stream s)
		{
			return ReadSz(s, Encoding.UTF8);
		}
		public static string ReadSz(Stream stream, Encoding encoding)
		{
			BinaryReader r = new BinaryReader(stream);
			List<byte> buff = new List<byte>();
			int b = 0;
			while ((b = stream.ReadByte()) != 0 && b != -1)
			{
				buff.Add((byte)b);
			}
			return encoding.GetString(buff.ToArray());
		}

		public static void WriteSz(string str, Stream stream)
		{
			WriteSz(str, stream, Encoding.UTF8);
		}
		public static void WriteSz(string str, Stream stream, Encoding encoding)
		{
			byte[] buff = encoding.GetBytes(str);
			stream.Write(buff, 0, buff.Length);
			stream.WriteByte(0);
		}
	}
}
