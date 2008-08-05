using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Hazzik.Net {
	public abstract class ServerBase : IDisposable {
		protected List<ClientBase> _clients = new List<ClientBase>();

		protected Socket _listenSocket;
		protected IPAddress _address;
		protected AddressFamily _addressFamily;
		protected string ip;
		protected int _port;
		protected string _name;

		private bool _disposed = false;

		protected ServerBase() { }

		public bool Start() {
			try {
				_listenSocket = new Socket(_addressFamily, SocketType.Stream, ProtocolType.Tcp);
				_listenSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
				_listenSocket.Listen(100);
				Console.WriteLine("{0} started, listen {1}:{2}", _name, _address, _port);
				Thread accept_tread = new Thread(new ThreadStart(AcceptTread));
				//accept_tread.IsBackground = true;
				accept_tread.Start();
			} catch(Exception e) {
				Console.WriteLine("Failed to list on port {0}\n{1}", _port, e.Message);
				_listenSocket = null;
				return false;
			}
			return true;
		}

		private void AcceptTread() {
			while(!_disposed) {
				OnAccept(_listenSocket.Accept());
			}
		}

		public abstract void OnAccept(Socket s);

		~ServerBase() {
			Dispose();
		}

		#region IDisposable Members

		public void Dispose() {
			if(!_disposed) {
				_disposed = true;
				//foreach (
				_listenSocket.Close();
				_listenSocket = null;
			}
		}


		#endregion
	}
}
