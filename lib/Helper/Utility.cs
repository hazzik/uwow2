using System;
using System.IO;

namespace Hazzik.Helper {
	public static class Utility {
		private static DateTime _deltaTime = new DateTime(1970, 1, 1);
		public static Random seed2 = new Random();

		public static int Random(int max) {
			return seed2.Next(max);
		}

		public static int Random(int min, int max) {
			return min < max ? seed2.Next(min, max) : min;
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
			try {
				return Convert.ToBoolean(value);
			}
			catch {
				return false;
			}
		}

		public static double ToDouble(string value) {
			try {
				return Convert.ToDouble(value);
			}
			catch {
				return 0D;
			}
		}

		public static int ToInt32(string value) {
			try {
				return value.StartsWith("0x") ? Convert.ToInt32(value.Substring(2), 16) : Convert.ToInt32(value);
			}
			catch {
				return 0;
			}
		}

		public static ulong ToUInt64(string value) {
			try {
				return value.StartsWith("0x") ? Convert.ToUInt64(value.Substring(2), 16) : Convert.ToUInt64(value);
			}
			catch {
				return 0;
			}
		}

		public static TimeSpan ToTimeSpan(string value) {
			try {
				return TimeSpan.Parse(value);
			}
			catch {
				return TimeSpan.Zero;
			}
		}

		public static void View(TextWriter tw, byte[] b, int offset, int len) {
			for(int i = 0; i < len; i += 16) {
				tw.Write("{0:X4}: ", i);
				for(int j = i; j < i + 16; j++) {
					if(j < len) {
						tw.Write("{0:X2} ", b[j + offset]);
					}
					else {
						tw.Write("-- ");
					}
					if(j == i + 7) {
						tw.Write("| ");
					}
				}
				tw.Write(": ");
				for(int j = i; (j < i + 16) && j + offset < b.Length; j++) {
					if(b[j + offset] < 32 || b[j + offset] > 127) {
						tw.Write(".");
					}
					else {
						tw.Write("{0}", ((char)(b[j + offset])));
					}
				}
				tw.WriteLine();
			}
		}

		public static byte[] Reverse(this byte[] from) {
			var res = new byte[from.Length];
			int i = 0;
			for(int t = from.Length - 1; t >= 0; t--) {
				res[i++] = from[t];
			}
			return res;
		}

		public static byte[] Concat(this byte[] a, byte[] b) {
			var tmp = new byte[a.Length + b.Length];
			Buffer.BlockCopy(a, 0, tmp, 0, a.Length);
			Buffer.BlockCopy(b, 0, tmp, a.Length, b.Length);
			return tmp;
		}

		public static bool Equals(byte[] left, byte[] right) {
			if(left.Length != right.Length) {
				return false;
			}
			for(int i = 0; i < left.Length; i++) {
				if(right[i] != left[i]) {
					return false;
				}
			}
			return true;
		}

		public static uint ToUnixTimestamp(DateTime time) {
			return (uint)(new TimeSpan(DateTime.Now.Ticks - _deltaTime.Ticks)).TotalSeconds;
		}
	}
}