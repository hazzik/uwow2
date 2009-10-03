using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	internal class WorldPacketSender : WorldClient, IPacketSender {
		public WorldPacketSender(Socket socket) : base(socket) {
		}

		public void Send(IPacket packet) {
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine((WMSG)packet.Code);
			Console.ForegroundColor = color;
			lock(this) {
				Stream data = GetStream();
				Stream head = firstPacket ? data : new CryptoStream(data, encryptor, CryptoStreamMode.Write);
				WriteSize(head, packet);
				WriteCode(head, packet);
				packet.WriteBody(data);
			}
		}

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