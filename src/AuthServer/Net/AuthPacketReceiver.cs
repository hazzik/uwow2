using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AuthPacketReceiver : AuthPacketReceiverBase, IPacketReceiver {
		public AuthPacketReceiver(Socket client) : base(client) {
		}

		#region IPacketReceiver Members

		public IPacket Receive() {
			Stream stream = GetStream();
			var reader = new BinaryReader(stream);
			int code = ReadCode(reader);
			int size = ReadSize(reader, code);
			var buffer = new byte[size];
			stream.Read(buffer, 0, buffer.Length);
			return new AuthPacket((RMSG)code, buffer);
		}

		#endregion
	}
}