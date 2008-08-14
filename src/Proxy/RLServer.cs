using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace UWoW {
	public class RLServer : ServerBase {
		private EndPoint _forwdEndPoint;
		private EndPoint _localEndPoint;

		public RLServer(int listen_port, string forward_host, int forward_port)
			: base() {
			_name = "RL PROXY";
			_port = listen_port;

			IPAddress forward_addr = Dns.GetHostEntry(forward_host).AddressList[0];
			_forwdEndPoint = new IPEndPoint(forward_addr, forward_port);
			_localEndPoint = new IPEndPoint(IPAddress.Any, listen_port);

			this.addressFamily = _localEndPoint.AddressFamily;
			this.Start();
		}

		public override void OnAccept(System.Net.Sockets.Socket s) {
			AddClient(new AuthProxy(s, _forwdEndPoint));
		}
	}
}
