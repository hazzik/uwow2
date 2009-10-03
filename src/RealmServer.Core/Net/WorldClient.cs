using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	public class WorldClient : IWorldClient, IPacketSender, IClient {
		protected ICryptoTransform decryptor;
		private ICryptoTransform encryptor;

		protected bool firstPacket = true;
		protected IPacketProcessor processor;
		protected Socket socket;

		public WorldClient(Socket socket) {
			this.socket = socket;
			processor = new WorldPacketProcessor(new Session(this));
		}

		#region IClient Members

		public virtual void Start() {
			try {
				while(true) {
					processor.Process(ReadPacket());
				}
			}
			catch(SocketException) {
			}
			catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			socket.Close();
		}

		#endregion

		#region IWorldClient Members

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

		public void SetSymmetricAlgorithm(SymmetricAlgorithm algorithm) {
			decryptor = algorithm.CreateDecryptor();
			encryptor = algorithm.CreateEncryptor();
			firstPacket = false;
		}

		#endregion

		public IPacket ReadPacket() {
			Stream data = GetStream();
			Stream head = firstPacket ? data : new CryptoStream(data, decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.Read(buffer, 0, buffer.Length);
			return new WorldPacket((WMSG)code, buffer);
		}

		protected static int ReadCode(Stream stream) {
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

		protected static int ReadSize(Stream stream) {
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

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}