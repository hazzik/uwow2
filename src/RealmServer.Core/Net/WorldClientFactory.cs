using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			return new AsyncClient(new AsyncWorldPaketReceiver(s), new WorldPacketProcessor(new Session(new WorldPacketSender(s))));
		}
	}
}