using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hazzik.Net {
	public class Server : IDisposable {
		private readonly IList<IClient> clients = new List<IClient>();
		private static readonly ManualResetEvent allDone = new ManualResetEvent(false);
		private readonly string name;
		private bool disposed;
		private Socket listenSocket;

		private IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
		private readonly IClientFactory clientFactory;

		private Server(string name, IClientFactory clientFactory, IPEndPoint localEndPoint) {
			this.name = name;
			this.localEndPoint = localEndPoint;
			this.clientFactory = clientFactory;
		}

		public IPEndPoint LocalEndPoint {
			get { return localEndPoint; }
			set { localEndPoint = value; }
		}

		public AddressFamily AddressFamily {
			get { return localEndPoint.AddressFamily; }
		}

		public IPAddress Address {
			get { return localEndPoint.Address; }
		}

		public int Port {
			get { return localEndPoint.Port; }
		}

		#region IDisposable Members

		public void Dispose() {
			if(disposed) {
				return;
			}
			disposed = true;
			listenSocket.Close();
			listenSocket = null;
		}

		#endregion

		public bool Start() {
			try {
				listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				listenSocket.Bind(localEndPoint);
				listenSocket.Listen(100);
				Console.WriteLine("{0} started, listen {1}", name, localEndPoint);
				AcceptTread();
			}
			catch(Exception e) {
				Console.WriteLine("Failed to list on {0}\n{1}", localEndPoint, e.Message);
				listenSocket = null;
				return false;
			}
			return true;
		}

		private void AcceptTread() {
			while(!disposed) {
				allDone.Reset();
				listenSocket.BeginAccept(ar => {
				                         	allDone.Set();
				                         	Accept(listenSocket.EndAccept(ar));
				                         }, null);
				allDone.WaitOne();
			}
		}

		private void Accept(Socket socket) {
			IClient client = clientFactory.Create(socket);
			client.Start();
			clients.Add(client);
		}

		~Server() {
			Dispose();
		}

		public static Server Create(string name, IPEndPoint localEndPoint, IClientFactory clientFactory) {
			return new Server(name, clientFactory, localEndPoint);
		}
	}
}