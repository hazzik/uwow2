using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UWoW {
	public class WorldServerProxy : ServerBase {
		#region ctors

		private void Init() {
			_name = "RW PROXY";
		}

		public WorldServerProxy(int listen_port, string forward_addr) {
			Init();

			_port = listen_port;
			IPHostEntry iPHostEntry1 = Dns.GetHostEntry("localhost");
			IPEndPoint iPEndPoint1 = new IPEndPoint(iPHostEntry1.AddressList[0], this._port);
			this.addressFamily = iPEndPoint1.Address.AddressFamily;
			this.Start();
		}

		public WorldServerProxy(int listen_port, IPEndPoint forward_point) {
			_name = "WORLD PROXY";
		}

		#endregion

		public override void OnAccept(System.Net.Sockets.Socket s) {
			AddClient(new WorldProxy(s));
		}
	}
}
