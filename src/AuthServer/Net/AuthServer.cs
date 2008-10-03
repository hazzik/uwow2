﻿using System;
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
			LocalEndPoint = new IPEndPoint(IPAddress.Any, 3724);
			this.Handler = new PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute>();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new AuthClient(this, s));
		}
	}
}