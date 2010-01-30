using System;
using System.IO;
using System.Net.Sockets;

namespace Hazzik.Net {
	public abstract class SocketHolder {
	    private readonly Socket socket;

	    protected SocketHolder(Socket socket) {
			this.socket = socket;
		}

	    protected virtual Stream GetStream() {
			return new NetworkStream(socket, false);
		}
	}
}