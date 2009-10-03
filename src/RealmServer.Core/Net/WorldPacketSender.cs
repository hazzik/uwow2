using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class WorldPacketSender : SocketHolder, IPacketSender {
		private readonly ICryptor cryptor;

		public WorldPacketSender(Socket socket, ICryptor cryptor) : base(socket) {
			this.cryptor = cryptor;
		}

		#region IPacketSender Members

		public void Send(IPacket packet) {
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine((WMSG)packet.Code);
			Console.ForegroundColor = color;
			lock(this) {
				Stream data = GetStream();
				Stream head = cryptor.EncryptStream(data);
				WriteSize(head, packet);
				WriteCode(head, packet);
				packet.WriteBody(data);
			}
		}

		#endregion

		private static void WriteCode(Stream head, IPacket packet) {
			head.WriteByte((byte)(packet.Code));
			head.WriteByte((byte)(packet.Code >> 0x08));
		}

		private static void WriteSize(Stream head, IPacket packet) {
			int size = packet.Size + 2;
			if(size > Int16.MaxValue) {
				head.WriteByte((byte)(size >> 0x10));
			}
			head.WriteByte((byte)(size >> 0x08));
			head.WriteByte((byte)(size));
		}
	}
}