using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			var cryptor = new Cryptor();
			var packetReceiver = new AsyncWorldPaketReceiver(s, cryptor);
			var packetSender = new WorldPacketSender(s, cryptor);
			return new AsyncClient(packetReceiver, new WorldPacketProcessor(new Session(packetSender), cryptor));
		}
	}
}