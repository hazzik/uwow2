using System;
using System.Net;
using System.Net.Sockets;
using Hazzik.Attributes;

namespace Hazzik.Net {
	public class WorldServer : ServerBase {
		public PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute> Handler { get; set; }

		public WorldServer() {
			_name = "WORLD SERVER";
			LocalEndPoint = new IPEndPoint(IPAddress.Any, 3725);
			Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
		}

		public override void OnAccept(Socket s) {
			_clients.Add(new WorldClient(this, s));
		}
	}
}