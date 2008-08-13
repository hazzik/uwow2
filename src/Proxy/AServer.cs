using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace UWoW
{
	public abstract class AServer: IDisposable
	{
		private List<AClient> _clients = new List<AClient>();

		protected Socket _listenSocket;
		protected AddressFamily addressFamily;

		protected int _port;
		protected string _name;

		private bool disposed = false;

		protected AServer()
		{ }

		public bool Start()
		{
			try
			{
				_listenSocket = new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
				_listenSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
				_listenSocket.Listen(100);
				Console.WriteLine("{0} started, listen {1}", _name, _listenSocket.LocalEndPoint);
				Thread accept_tread = new Thread(new ThreadStart(acceptTread));
				accept_tread.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to list on port {0}\n{1}", this._port, e.Message);
				this._listenSocket = null;
				return false;
			}
			return true;
		}

		private void acceptTread()
		{
			while (!disposed)
			{
				Socket s = _listenSocket.Accept();
				OnAccept(s);
			}
		}

		public abstract void OnAccept(Socket s);

		protected void AddClient(AClient c)
		{
			if (!_clients.Contains(c))
				_clients.Add(c);
		}

		protected void DelClient(AClient c)
		{
			_clients.Remove(c);
		}
		
		#region Finalization Methods

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				_listenSocket.Close();
				_listenSocket = null;
			}
		}

		~AServer()
		{
			Dispose();
		}

		#endregion
	}
}
