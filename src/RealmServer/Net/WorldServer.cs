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
			LocalEndPoint = new IPEndPoint(IPAddress.Any, 3725);
			Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new WorldClient(this, s));
		}
	}
}