using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Hazzik.Net {
	internal class AsyncWorldClient : WorldClient, IAsyncPacketReader {
		public AsyncWorldClient(Socket socket) : base(socket) {
		}

		public override void Start() {
			try {
				ReadPacketAsync(packet => {
				                	processor.Process(packet);
				                	Start();
				                });
			}
			catch(SocketException) {
			}
			catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
			//socket.Close();
		}

		public void ReadPacketAsync(Action<IPacket> callback) {
			Stream data = GetStream();
			Stream head = firstPacket ? data : new CryptoStream(data, decryptor, CryptoStreamMode.Read);

			int size = ReadSize(head);
			int code = ReadCode(head);

			var buffer = new byte[size - 4];
			data.ReadAsync(buffer, 0, buffer.Length, () => callback(new WorldPacket((WMSG)code, buffer)));
		}
	}
}