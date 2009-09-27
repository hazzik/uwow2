using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class ClientAcceptor {
		private readonly IClientFactory clientFacory;
		private readonly IList<ClientBase> clients = new List<ClientBase>();

		public ClientAcceptor(IClientFactory clientFacory) {
			this.clientFacory = clientFacory;
		}

		public void OnAccept(Socket s) {
			ClientBase client = clientFacory.Create(s);
			client.Start();
			clients.Add(client);
		}
	}
}