using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class AuthClientAcceptor : IClientAcceptor {
		protected IList<ClientBase> _clients = new List<ClientBase>();

		#region IClientAcceptor Members

		public void OnAccept(Socket s) {
			var client = new AuthClient(s);
			client.SetProcessor(new AuthPacketProcessor(client));
			client.Start();
			_clients.Add(client);
		}

		#endregion
	}
}