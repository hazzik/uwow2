using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hazzik.Net {
	public class Server : IDisposable {
		private static readonly ManualResetEvent allDone = new ManualResetEvent(false);
		private readonly ClientAcceptor _acceptor;
		private bool _disposed;
		protected Socket _listenSocket;

		private IPEndPoint _localEndPoint = new IPEndPoint(IPAddress.Any, 0);
		protected string _name;

		public Server(string name, ClientAcceptor acceptor, IPEndPoint localEndPoint) {
			_name = name;
			_localEndPoint = localEndPoint;
			_acceptor = acceptor;
		}

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

		private void AcceptTread() {
			while(!_disposed) {
				allDone.Reset();
				_listenSocket.BeginAccept(ar => {
				                          	allDone.Set();
				                          	_acceptor.OnAccept(_listenSocket.EndAccept(ar));
				                          }, null);
				allDone.WaitOne();
			}
		}

		~Server() {
			Dispose();
		}
	}
}