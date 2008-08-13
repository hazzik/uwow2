using System;
using System.Net;
using System.Net.Sockets;

namespace UWoW
{
	public abstract class AClient
	{
		protected Socket _socket;

		public AClient(Socket s)
		{

		}
		public AClient()
		{

		}
	}
	public abstract class AProxyClient : AClient
	{
		protected Socket _socket_forward;

		public AProxyClient(Socket s, string forward_host, int forward_port)
			: base(s)
		{
			Dns.GetHostEntry(forward_host);
			//EndPoint point = new IPEndPoint (new IPAddress (
		}

		public AProxyClient(Socket s, EndPoint forward_point)
		{

		}
	}
}
				  