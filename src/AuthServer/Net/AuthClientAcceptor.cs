using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientAcceptor : IClientAcceptor {
		private readonly IList<ClientBase> clients = new List<ClientBase>();

		#region IClientAcceptor Members

		public void OnAccept(Socket s) {
			AuthClient client = AuthClient.Create(s);
			client.Start();
			clients.Add(client);
		}

		#endregion
	}
}