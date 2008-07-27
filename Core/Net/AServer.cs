using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace UWoW.Net
{
	public abstract class AServer: IDisposable
	{
		protected static List<AClient> clients = new List<AClient>();

		protected Socket listenSocket;
		protected IPAddress address;
		protected AddressFamily addressFamily;
		protected string ip;
		protected int port;
		protected string name;

		private bool disposed = false;

		protected AServer()
		{ }

		public bool Start()
		{
			try
			{
				listenSocket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
				listenSocket.Bind(new IPEndPoint(IPAddress.Any, port));
				listenSocket.Listen(100);
				Console.WriteLine("{0} started, listen {1}:{2}", name, address, port);
				Thread accept_tread = new Thread(new ThreadStart(AcceptTread));
				accept_tread.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to list on port {0}\n{1}", this.port, e.Message);
				this.listenSocket = null;
				return false;
			}
			return true;
		}

		private void AcceptTread()
		{
			while (!disposed)
			{
				Socket s = listenSocket.Accept();
				OnAccept(s);
			}
		}

		public abstract void OnAccept(Socket s);
		
		~AServer()
		{
			Dispose();
		}
		#region IDisposable Members

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				//foreach (
				listenSocket.Close();
				listenSocket = null;
			}
		}


		#endregion
	}
}
