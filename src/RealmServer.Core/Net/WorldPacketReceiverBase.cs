using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class WorldPacketReceiverBase : WorldClient {
		public WorldPacketReceiverBase(Socket socket) : base(socket) {
		}

		protected static int ReadCode(Stream stream) {
			int code = 0;
			code |= stream.ReadByte();
			code |= stream.ReadByte() << 0x08;
			code |= stream.ReadByte() << 0x10;
			code |= stream.ReadByte() << 0x18;
			return code;
		}

		protected static int ReadSize(Stream stream) {
			int size = stream.ReadByte();
			if((size & 0x80) != 0x00) {
				size &= 0x7f;
				size = (size << 0x08) | stream.ReadByte();
			}
			size = (size << 0x08) | stream.ReadByte();
			return size;
		}
	}
}