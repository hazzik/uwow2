using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UWoW {
	public class AServerGeneric<T> : IDisposable
		where T : ClientBase, new() {
		private EndPoint _localEndpoint;
		private String _name;
		private Thread _acceptThread;
		private Socket _listenSocket;

		private bool _disposed;

		#region ctors

		public AServerGeneric(string name, int port) {
			_localEndpoint = new IPEndPoint(IPAddress.Any, port);
		}

		#endregion

		#region Methods

		private void __acceptTread() {
			while(!_disposed) {
				Socket s = _listenSocket.Accept();
				T t = new T();
			}
		}

		public void Start() {
			try {
				_listenSocket = new Socket(_localEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				_listenSocket.Bind(_localEndpoint);
				_listenSocket.Listen(1);
				Console.WriteLine("{0} started, listen {1}", _name, _localEndpoint);

				_acceptThread = new Thread(__acceptTread);
				_acceptThread.Start();
			} catch(Exception e) {
				this.Dispose();
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose() {
			if(!_disposed) {
				if(null != _acceptThread) {
					_acceptThread.Abort();
					_acceptThread = null;
				}
				if(null != _listenSocket) {
					_listenSocket.Shutdown(SocketShutdown.Both);
					_listenSocket.Close();
					_listenSocket = null;
				}
				if(null != _localEndpoint) {
					_localEndpoint = null;
				}
				_disposed = true;
			}
		}

		#endregion
	}

	public class RWServer : ServerBase {
		#region ctors

		private void Init() {
			_name = "RW PROXY";
		}

		public RWServer(int listen_port, string forward_addr) {
			Init();

			_port = listen_port;
			IPHostEntry iPHostEntry1 = Dns.GetHostEntry("localhost");
			IPEndPoint iPEndPoint1 = new IPEndPoint(iPHostEntry1.AddressList[0], this._port);
			this.addressFamily = iPEndPoint1.Address.AddressFamily;
			this.Start();
		}

		public RWServer(int listen_port, IPEndPoint forward_point) {
			_name = "RW PROXY";
		}

		#endregion

		public override void OnAccept(System.Net.Sockets.Socket s) {
			AddClient(new WorldProxy(s));
		}
	}
}
