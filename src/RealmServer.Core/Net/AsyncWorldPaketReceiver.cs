using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncWorldPaketReceiver : WorldPacketReceiverBase, IAsyncPacketReceiver {
		public AsyncWorldPaketReceiver(Socket socket, ICryptor cryptor) : base(socket, cryptor) {
		}

		#region IAsyncPacketReceiver Members

		public void ReceiveAsync(Action<IPacket> callback) {
			Stream data = GetStream();
			var head = new BinaryReader(cryptor.DecryptStream(data));
			int size = ReadSize(head);
			int code = ReadCode(head);
			var buffer = new byte[size - 4];
			data.ReadAsync(buffer, 0, buffer.Length, () => callback(new WorldPacket((WMSG)code, buffer)));
		}

		#endregion
	}
}