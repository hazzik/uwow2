using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientBase : IPacketSender {
		protected Socket socket;

		public AuthClientBase(Socket client) {
			socket = client;
		}

		#region IPacketSender Members

		public void Send(IPacket packet) {
			Stream data = GetStream();
			Stream head = data;

			WriteCode(head, packet);
			WriteSize(head, packet);
			packet.WriteBody(data);
		}

		#endregion

		private static void WriteCode(Stream stream, IPacket packet) {
			stream.WriteByte((byte)packet.Code);
		}

		public static void WriteSize(Stream stream, IPacket packet) {
			if((RMSG)packet.Code == RMSG.REALM_LIST || (RMSG)packet.Code == RMSG.XFER_DATA) {
				stream.WriteByte((byte)(packet.Size));
				stream.WriteByte((byte)(packet.Size >> 0x08));
			}
		}

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}