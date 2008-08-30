using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik {
	public static class BinaryWriterExtension {
		public static void WriteCString(this BinaryWriter w, string value) {
			WriteCString(w, value, Encoding.UTF8);
		}

		public static void WriteCString(this BinaryWriter w, string value, Encoding encoding) {
			if(!string.IsNullOrEmpty(value)) {
				w.Write(encoding.GetBytes(value));
			}
			w.Write((byte)0);
		}

		public static void WritePackGuid(this BinaryWriter w, ulong guid) {
			var buff = new byte[8];
			var mask = (byte)0;
			var offset = 0;
			for(int i = 0; i < 8; i++) {
				if((byte)guid != 0) {
					buff[offset++] = (byte)guid;
					mask |= (byte)(1 << i);
				}
				guid >>= 8;
			}
			w.Write(mask);
			w.Write(buff, 0, offset);
		}
	}
}
