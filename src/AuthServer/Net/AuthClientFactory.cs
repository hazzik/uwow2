using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			return new AsyncClient(new AsyncAuthPacketReceiver(s), new AuthPacketProcessor(new AuthPacketSender(s)));
		}
	}
}