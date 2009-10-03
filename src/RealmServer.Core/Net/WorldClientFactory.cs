using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			var asyncPacketReader = new AsyncWorldPaketReceiver(s);
			return new AsyncClient(asyncPacketReader, new WorldPacketProcessor(new Session(asyncPacketReader)));
		}
	}
}