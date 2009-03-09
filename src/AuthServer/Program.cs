using System;
using System.Net;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		private static void Main(string[] args) {
			var server = new Server("AUTH SERVER", new AuthClientAcceptor(), new IPEndPoint(IPAddress.Any, 3724));
			server.Start();
			Console.ReadLine();
		}
	}
}