using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	internal class AsyncWorldPaketReceiver : WorldPacketReceiverBase, IAsyncPacketReceiver {
		public AsyncWorldPaketReceiver(Socket socket) : base(socket) {
		}

		#region IAsyncPacketReceiver Members

		public void ReceiveAsync(Action<IPacket> callback) {
			Stream data = GetStream();
			Stream head = firstPacket ? data : new CryptoStream(data, decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.ReadAsync(buffer, 0, buffer.Length, () => callback(new WorldPacket((WMSG)code, buffer)));
		}

		#endregion
	}
}