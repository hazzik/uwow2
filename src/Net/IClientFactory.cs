using System;
using System.Net.Sockets;

namespace Hazzik.Net {
	public interface IClientFactory {
		ClientBase Create(Socket s);
	}
}