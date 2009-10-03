using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AuthPacketReceiver : AuthClientBase, IPacketReceiver {
		public AuthPacketReceiver(Socket client) : base(client) {
		}

		#region IPacketReceiver Members

		public IPacket Receive() {
			Stream stream = GetStream();
			int code = ReadCode(stream);
			int size = ReadSize(stream, code);
			var buffer = new byte[size];
			stream.Read(buffer, 0, buffer.Length);
			return new AuthPacket((RMSG)code, buffer);
		}

		#endregion
	}
}