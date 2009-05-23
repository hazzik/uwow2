using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public class WorldClient : ClientBase, IWorldClient {
		private ICryptoTransform _decryptor;
		private ICryptoTransform _encryptor;

		private bool _firstPacket = true;

		public WorldClient(Socket socket)
			: base(socket) {
		}

		#region IWorldClient Members

		public override void Send(IPacket packet) {
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine((WMSG)packet.Code);
			Console.ForegroundColor = color;
			lock(this) {
				Stream data = GetStream();
				Stream head = _firstPacket ? data : new CryptoStream(data, _encryptor, CryptoStreamMode.Write);
				WriteSize(head, packet);
				WriteCode(head, packet);
				packet.WriteBody(data);
			}
		}

		public void SetSymmetricAlgorithm(SymmetricAlgorithm algorithm) {
			_decryptor = algorithm.CreateDecryptor();
			_encryptor = algorithm.CreateEncryptor();
			_firstPacket = false;
		}

		#endregion

		public override IPacket ReadPacket() {
			Stream data = GetStream();
			Stream head = _firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.Read(buffer, 0, buffer.Length);
			return new WorldPacket((WMSG)code, buffer);
		}

		public override void ReadPacketAsync(Action<IPacket> func) {
			Stream data = GetStream();
			Stream head = _firstPacket ? data : new CryptoStream(data, _decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.ReadAsync(buffer, 0, buffer.Length, () => func(new WorldPacket((WMSG)code, buffer)));
		}

		private static int ReadCode(Stream stream) {
			int code = 0;
			code |= stream.ReadByte();
			code |= stream.ReadByte() << 0x08;
			code |= stream.ReadByte() << 0x10;
			code |= stream.ReadByte() << 0x18;
			return code;
		}

		private static void WriteCode(Stream head, IPacket packet) {
			head.WriteByte((byte)(packet.Code));
			head.WriteByte((byte)(packet.Code >> 0x08));
		}

		private static int ReadSize(Stream stream) {
			int size = stream.ReadByte();
			if((size & 0x80) != 0x00) {
				size &= 0x7f;
				size = (size << 0x08) | stream.ReadByte();
			}
			size = (size << 0x08) | stream.ReadByte();
			return size;
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