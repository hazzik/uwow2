using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	internal class WorldPacketReceiver : WorldClient, IPacketReceiver {
		public WorldPacketReceiver(Socket socket) : base(socket) {
		}

		#region IPacketReceiver Members

		public IPacket Receive() {
			Stream data = GetStream();
			Stream head = firstPacket ? data : new CryptoStream(data, decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.Read(buffer, 0, buffer.Length);
			return new WorldPacket((WMSG)code, buffer);
		}

		#endregion
	}
}