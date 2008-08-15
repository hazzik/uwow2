using System;
using System.Net;
using System.Net.Sockets;

namespace UWoW {
	public abstract class ClientBase {
		protected Socket _socket;

		public ClientBase(Socket s) {

		}
		public ClientBase() {

		}
	}
}
				  