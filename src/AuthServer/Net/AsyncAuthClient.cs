using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	internal class AsyncAuthClient : AuthClient {
		public AsyncAuthClient(Socket client) : base(client) {
		}
	}
}