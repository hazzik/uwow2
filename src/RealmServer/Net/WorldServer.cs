using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using System.Net;
using Hazzik.Attributes ;

namespace Hazzik.Net {
	public class WorldServer : ServerBase {
		public PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute> Handler { get; set; }
		public WorldServer()
			: base() {
			_name = "WORLD SERVER";
			_port = 3725;
			var ep = new IPEndPoint(IPAddress.Any, _port);
			_addressFamily = ep.Address.AddressFamily;
			_address = ep.Address;

			Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new WorldClient(this, s));
		}
	}
}