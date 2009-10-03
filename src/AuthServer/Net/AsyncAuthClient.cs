using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncAuthClient : AuthClient, IAsyncPacketReader {
		public AsyncAuthClient(Socket client) : base(client) {
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
			Stream stream = GetStream();
			int code = ReadCode(stream);
			int size = ReadSize(stream, code);
			var buffer = new byte[size];
			stream.ReadAsync(buffer, 0, buffer.Length, () => callback(new AuthPacket((RMSG)code, buffer)));
		}
	}
}