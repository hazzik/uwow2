using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hazzik.Net {
	public class WorldClientAcceptor : IClientAcceptor {
		protected IList<ClientBase> _clients = new List<ClientBase>();

		#region IClientAcceptor Members

		public void OnAccept(Socket s) {
			var client = new WorldClient(s);
			client.SetProcessor(new WorldPacketProcessor(new Session(client)));
			client.Start();	
			_clients.Add(client);
		}

		#endregion
	}
}