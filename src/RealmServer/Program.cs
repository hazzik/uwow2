using System;
using System.Net;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik {
	internal class Program {
		private static void Main(string[] args) {
			WorldClient.Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
			foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				WorldClient.Handler.AddAssembly(assembly);
			}
			WorldClient.Handler.Load();
			var server = new Server("WORLD SERVER", new WorldClientAcceptor(), new IPEndPoint(IPAddress.Any, 3725));
			server.Start();
			Console.ReadLine();
		}
	}
}