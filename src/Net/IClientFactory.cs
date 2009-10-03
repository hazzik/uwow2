using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public interface IClientFactory {
		IClient Create(Socket s);
	}
}