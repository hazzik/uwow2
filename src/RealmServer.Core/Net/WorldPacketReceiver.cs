using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class WorldPacketReceiver : WorldPacketReceiverBase, IPacketReceiver {
		public WorldPacketReceiver(Socket socket, ICryptor cryptor) : base(socket, cryptor) {
		}

		#region IPacketReceiver Members

		public IPacket Receive() {
			Stream data = GetStream();
			Stream head = cryptor.DecryptStream(data);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.Read(buffer, 0, buffer.Length);
			return new WorldPacket((WMSG)code, buffer);
		}

		#endregion
	}
}