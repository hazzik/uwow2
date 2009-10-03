using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AuthPacketSender : AuthClientBase, IPacketSender {
		public AuthPacketSender(Socket client) : base(client) {
		}

		public void Send(IPacket packet) {
			Stream data = GetStream();
			Stream head = data;

			WriteCode(head, packet);
			WriteSize(head, packet);

			packet.WriteBody(data);
		}

		protected static void WriteCode(Stream stream, IPacket packet) {
			stream.WriteByte((byte)packet.Code);
		}

		protected static void WriteSize(Stream stream, IPacket packet) {
			if((RMSG)packet.Code == RMSG.REALM_LIST || (RMSG)packet.Code == RMSG.XFER_DATA) {
				stream.WriteByte((byte)(packet.Size));
				stream.WriteByte((byte)(packet.Size >> 0x08));
			}
		}
	}
}