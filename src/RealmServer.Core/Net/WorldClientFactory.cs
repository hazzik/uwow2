using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientFactory : IClientFactory {
		public IClient Create(Socket s) {
			return new WorldClient(s);
		}
	}
}