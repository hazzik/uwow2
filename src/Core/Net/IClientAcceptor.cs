using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public interface IClientAcceptor {
		void OnAccept(Socket s);
	}
}