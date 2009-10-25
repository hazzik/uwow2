using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class WorldPacketReceiverBase : SocketHolder {
		protected readonly ICryptor cryptor;

		protected WorldPacketReceiverBase(Socket socket, ICryptor cryptor) : base(socket) {
			this.cryptor = cryptor;
		}

		protected static int ReadCode(BinaryReader reader) {
			int code = 0;
			code |= reader.ReadByte();
			code |= reader.ReadByte() << 0x08;
			code |= reader.ReadByte() << 0x10;
			code |= reader.ReadByte() << 0x18;
			return code;
		}

		protected static int ReadSize(BinaryReader reader) {
			int size = reader.ReadByte();
			if((size & 0x80) != 0x00) {
				size &= 0x7f;
				size = (size << 0x08) | reader.ReadByte();
			}
			size = (size << 0x08) | reader.ReadByte();
			return size;
		}
	}
}