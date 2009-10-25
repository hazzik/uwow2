using System;
using System.IO;

namespace Hazzik.Net {
	public class Client : IClient {
		private readonly IPacketProcessor packetProcessor;
		private readonly IPacketReceiver packetReceiver;

		public Client(IPacketReceiver packetReceiver, IPacketProcessor packetProcessor) {
			this.packetReceiver = packetReceiver;
			this.packetProcessor = packetProcessor;
		}

		#region IClient Members

		public virtual void Start() {
			try {
				while(true) {
					packetProcessor.Process(packetReceiver.Receive());
				}
			}
			catch(EndOfStreamException) {
			}
			catch(Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}

		#endregion
	}
}