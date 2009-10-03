using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class SocketHolder {
		protected Socket socket;

		public SocketHolder(Socket socket) {
			this.socket = socket;
		}

		public virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}