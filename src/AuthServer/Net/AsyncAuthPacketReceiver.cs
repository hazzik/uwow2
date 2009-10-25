using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncAuthPacketReceiver : AuthPacketReceiverBase, IAsyncPacketReceiver {
		public AsyncAuthPacketReceiver(Socket client) : base(client) {
		}

		#region IAsyncPacketReceiver Members

		public void ReceiveAsync(Action<IPacket> callback) {
			Stream stream = GetStream();
			var reader = new BinaryReader(stream);

			int code = ReadCode(reader);
			int size = ReadSize(reader, code);
			
			var buffer = new byte[size];
			stream.ReadAsync(buffer, 0, buffer.Length, () => callback(new AuthPacket((RMSG)code, buffer)));
		}

		#endregion
	}
}