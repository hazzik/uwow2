using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AsyncClient : IClient {
		private readonly IAsyncPacketReceiver asyncPacketReceiver;
		private readonly IPacketProcessor packetProcessor;

		public AsyncClient(IAsyncPacketReceiver asyncPacketReceiver, IPacketProcessor packetProcessor) {
			this.asyncPacketReceiver = asyncPacketReceiver;
			this.packetProcessor = packetProcessor;
		}

		#region IClient Members

		public void Start() {
			try {
				asyncPacketReceiver.ReceiveAsync(packet => {
				                                  	packetProcessor.Process(packet);
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

		#endregion
	}
}