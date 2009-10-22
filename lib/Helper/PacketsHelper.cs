using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Hazzik {
	public class PacketsHelper {
		public static byte[] GetBytes(string pkt) {
			var r = new StringReader(pkt);
			var sb = new StringBuilder();
			string str;
			while((str = r.ReadLine()) != null) {
				int i = 0;
				try {
					i = Convert.ToInt32(str.Substring(0, 4), 16);
				}
				catch {
				}
				if(i == 0) {
					if(sb.Length > 0) {
						sb.Replace(" ", "").Replace("-", "").Replace("\r", "").Replace("|", "");
						return HexToBytes(sb.ToString());
					}
					sb.Length = 0;
				}
				string[] strArr3 = str.Split(':');
				if(strArr3.Length > 2) {
					sb.Append(strArr3[1]);
				}
			}
			if(sb.Length > 0) {
				sb.Replace(" ", "").Replace("-", "").Replace("\r", "").Replace("|", "");
				return HexToBytes(sb.ToString());
			}
			return null;
		}

		private static byte[] HexToBytes(string src) {
			var tmp = new List<byte>();
			for(int i = 0; i < src.Length; i += 2) {
				tmp.Add(byte.Parse(src.Substring(i, 2), NumberStyles.HexNumber));
			}
			return tmp.ToArray();
		}
	}
}