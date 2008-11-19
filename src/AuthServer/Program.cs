using System;
using System.Reflection;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		private static void Main(string[] args) {
			var server = new AuthServer();
			server.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			server.Handler.Load();
			server.Start();
			Console.ReadLine();
		}
	}
}