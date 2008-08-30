using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik {
	public static class BinaryReaderExtension {
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

		public static ulong ReadPackGuid(this BinaryReader reader) {
			ulong guid = 0;
			byte mask = reader.ReadByte();
			for(int i = 0; i < 8; i++) {
				guid >>= 8;
				if((mask & (1 << i)) != 0) {
					guid |= (ulong)(reader.ReadByte()) << 56;
				}
			}
			return guid;
		}
 	}
}
