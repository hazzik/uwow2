using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncAuthClient : AuthClient {
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
	}
}