using System;
using System.Net;
using System.Net.Sockets;
using Hazzik.Attributes;

namespace Hazzik.Net {
	public class AuthServer : ServerBase {
		public PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute> Handler { get; set; }

		public AuthServer()
			: base() {
			_name = "AUTH SERVER";
			LocalEndPoint = new IPEndPoint(IPAddress.Any, 3724);
			Handler = new PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute>();
		}

		public override void OnAccept(Socket s) {
			_clients.Add(new AuthClient(this, s));
		}
	}
}