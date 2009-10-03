using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientBase {
		protected Socket socket;

		public AuthClientBase(Socket client) {
			socket = client;
		}

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}