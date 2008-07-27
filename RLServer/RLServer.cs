using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using UWoW.Net;

namespace UWoW
{
	public class RLServer : AServer
	{
		public RLServer()
			: base()
		{
			this.name = "RL SERVER";
			this.port = 3724;
			IPHostEntry iPHostEntry1 = Dns.GetHostEntry("localhost");
			IPEndPoint iPEndPoint1 = new IPEndPoint(iPHostEntry1.AddressList[0], this.port);
			this.addressFamily = iPEndPoint1.Address.AddressFamily;
			this.address = iPEndPoint1.Address;
			this.Start();
		}

		public override void OnAccept(System.Net.Sockets.Socket s)
		{
			RLServer.clients.Add(new RLSClient(s));
		}
	}
}
