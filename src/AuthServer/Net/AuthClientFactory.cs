using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			var asyncPacketReader = new AsyncAuthPacketReceiver(s);
			return new AsyncClient(asyncPacketReader, new AuthPacketProcessor(asyncPacketReader));
		}
	}
}