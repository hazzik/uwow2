using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using UWoW.Net;

namespace UWoW {
	public class RLServer : AServer {
		public RLServer()
			: base() {
			_name = "RL SERVER";
			_port = 3724;
			var ep = new IPEndPoint(IPAddress.Any, _port);
			_addressFamily = ep.Address.AddressFamily;
			_address = ep.Address;
			this.Start();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			_clients.Add(new RLSClient(s));
		}
	}
}