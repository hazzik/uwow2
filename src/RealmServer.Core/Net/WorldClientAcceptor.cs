using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientAcceptor : IClientAcceptor {
		protected IList<ClientBase> clients = new List<ClientBase>();

		#region IClientAcceptor Members

		public void OnAccept(Socket s) {
			WorldClient client = WorldClient.Create(s);
			client.Start();
			clients.Add(client);
		}

		#endregion
	}
}