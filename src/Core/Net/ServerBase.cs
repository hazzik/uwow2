using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hazzik.Net {
	public abstract class ServerBase : IDisposable {
		protected List<ClientBase> _clients = new List<ClientBase>();
		protected Socket _listenSocket;
		protected string _name;

		private IPEndPoint _localEndPoint = new IPEndPoint(IPAddress.Any, 0);

		public IPEndPoint LocalEndPoint {
			get { return _localEndPoint; }
			set { _localEndPoint = value; }
		}

		public AddressFamily AddressFamily {
			get { return _localEndPoint.AddressFamily; }
		}

		public IPAddress Address {
			get { return _localEndPoint.Address; }
		}

		public int Port {
			get { return _localEndPoint.Port; }
		}

		private bool _disposed;

		public bool Start() {
			try {
				_listenSocket = new Socket(_localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				_listenSocket.Bind(_localEndPoint);
				_listenSocket.Listen(100);
				Console.WriteLine("{0} started, listen {1}", _name, _localEndPoint);
				AcceptTread();
			}
			catch(Exception e) {
				Console.WriteLine("Failed to list on {0}\n{1}", _localEndPoint, e.Message);
				_listenSocket = null;
				return false;
			}
			return true;
		}

		private static readonly ManualResetEvent allDone = new ManualResetEvent(false);

		private void AcceptTread() {
			while(!_disposed) {
				allDone.Reset();
				_listenSocket.BeginAccept(ar => {
				                          	allDone.Set();
				                          	var accept = _listenSocket.EndAccept(ar);
				                          	OnAccept(accept);
				                          }, null);
				allDone.WaitOne();
			}
		}

		public abstract void OnAccept(Socket s);

		~ServerBase() {
			Dispose();
		}

		#region IDisposable Members

		public void Dispose() {
			if(_disposed) {
				return;
			}
			_disposed = true;
			_listenSocket.Close();
			_listenSocket = null;
		}

		#endregion
	}
}