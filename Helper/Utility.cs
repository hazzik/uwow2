using System.Collections;
using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Helper {
	public static class Utility {

		private static DateTime deltaTime = new DateTime(1970, 1, 1);

		public static Random seed2 = new Random();

		public static int Random(int max) {
			return seed2.Next(max);
		}

		public static int Random(int min, int max) {
			if(min < max)
				return seed2.Next(min, max);
			return min;
		}

		public static int Random4() {
			return seed2.Next() & 0x00000003;
		}

		public static int Random8() {
			return seed2.Next() & 0x00000007;
		}

		public static int Random16() {
			return seed2.Next() & 0x0000000F;
		}

		public static int Random1024() {
			return seed2.Next() & 0x000003FF;
		}

		public static int Random1024x1024() {
			return seed2.Next() & 0x000FFFFF;
		}

		public static double RandomDouble() {
			return seed2.NextDouble();
		}

		public static bool ToBoolean(string value) {
			bool val;
			try {
				val = Convert.ToBoolean(value);
			} catch {
				val = false;
			}
			return val;
		}

		public static double ToDouble(string value) {
			double val;
			try {
				val = Convert.ToDouble(value);
			} catch {
				val = 0D;
			}
			return val;
		}

		public static int ToInt32(string value) {
			int val;
			try {
				if(value.StartsWith("0x"))
					return Convert.ToInt32(value.Substring(2), 16);
				else
					val = Convert.ToInt32(value);
			} catch {
				val = 0;
			}
			return val;
		}

		public static ulong ToUInt64(string value) {
			ulong val;
			try {
				if(value.StartsWith("0x"))
					return Convert.ToUInt64(value.Substring(2), 16);
				else
					val = Convert.ToUInt64(value);
			} catch {
				val = 0;
			}
			return val;
		}

		public static TimeSpan ToTimeSpan(string value) {
			TimeSpan val;
			try {
				val = TimeSpan.Parse(value);
			} catch {
				val = TimeSpan.Zero;
			}
			return val;
		}

		public static uint ToUnixTimestamp(DateTime d) {
			return (uint)(new TimeSpan(DateTime.Now.Ticks - deltaTime.Ticks)).TotalSeconds;
		}

		public static DateTime ToDateTime(uint unixTimestamp) {
			return deltaTime.AddSeconds((double)unixTimestamp);
		}

		public static void View(TextWriter tw, byte[] b, int offset, int len) {
			int i1;
			int i2;
			int i3;
			for(i1 = 0; (i1 < len); i1 += 16) {
				tw.Write("{0:X10}: ", i1);
				for(i2 = i1; ((i2 < (i1 + 16)) && ((i2 + offset) < b.Length)); i2++) {
					if(i2 >= len)
						tw.Write("-- ");
					else
						tw.Write("{0:X2} ", b[i2 + offset]);
					if(i2 == i1 + 7)
						tw.Write("| ");
				}
				tw.Write(" ");
				for(i3 = i1; (((i3 + offset) < b.Length) && ((i3 < (i1 + 16)) && (i3 < len))); i3++)
					if(b[i3 + offset] < 32 || b[i3 + offset] > 127)
						tw.Write(".");
					else
						tw.Write("{0}", ((char)(b[i3 + offset])).ToString());

				tw.WriteLine();
			}
		}


		public static byte[] Reverse(this byte[] from) {
			byte[] res = new byte[from.Length];
			int i = 0;
			for(int t = from.Length - 1; t >= 0; t--)
				res[i++] = from[t];
			return res;
		}

		public static byte[] Concat(this byte[] a, byte[] b) {
			byte[] tmp = new byte[a.Length + b.Length];
			Buffer.BlockCopy(a, 0, tmp, 0, a.Length);
			Buffer.BlockCopy(b, 0, tmp, a.Length, b.Length);
			return tmp;
		}

		public static void WriteCString(this BinaryWriter w, string value) {
			WriteCString(w, value, Encoding.UTF8);
		}

		public static void WriteCString(this BinaryWriter w, string value, Encoding encoding) {
			w.Write(encoding.GetBytes(value));
			w.Write((byte)0);
		}

		public static string ReadCString(this BinaryReader r) {
			return ReadCString(r, Encoding.UTF8);
		}

		public static string ReadCString(this BinaryReader r, Encoding encoding) {
			var b = (byte)0;
			var buff = new List<byte>();
			while((b = r.ReadByte()) != 0) {
				buff.Add(b);
			}
			return encoding.GetString(buff.ToArray());
		}
	}
}