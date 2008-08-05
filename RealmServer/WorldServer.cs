using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using System.Net;

namespace Hazzik {
	public class WorldServer : ServerBase {
		public WorldServer()
			: base() {
			_name = "WORLD SERVER";
			_port = 3725;
			var ep = new IPEndPoint(IPAddress.Any, _port);
			_addressFamily = ep.Address.AddressFamily;
			_address = ep.Address;
			this.Start();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new WorldClient(s));
		}
	}
}