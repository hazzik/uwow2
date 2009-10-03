using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncWorldClient : WorldClient {
		public AsyncWorldClient(Socket socket) : base(socket) {
		}
	}
}