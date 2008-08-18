using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using Hazzik.Net;
using Hazzik.Attributes;

namespace Hazzik.Net {
	public class AuthServer : ServerBase {
		public PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute> Handler { get; set; }
		public AuthServer()
			: base() {
			_name = "AUTH SERVER";
			_port = 3724;
			var ep = new IPEndPoint(IPAddress.Any, _port);
			_addressFamily = ep.Address.AddressFamily;
			_address = ep.Address;
			this.Handler = new PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute>();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new AuthClient(this, s));
		}
	}
}