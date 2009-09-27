using System;
using System.Net;
using Hazzik.Net;

namespace Hazzik {
	internal class App {
		private static void Main(string[] args) {
			var server = Server.Create("AUTH SERVER", new IPEndPoint(IPAddress.Any, 3724), new AuthClientFactory());
			server.Start();
			Console.ReadLine();
		}
	}
}