using System;
using System.Net;
using System.Reflection;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik {
	internal class Program {
		private static void Main(string[] args) {
			ServiceLocator.Initialize(new StructureMapServiceLocator());
			WorldClient.Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
			foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				WorldClient.Handler.AddAssembly(assembly);
			}
			WorldClient.Handler.Load();
			var server = new Server("WORLD SERVER", new WorldClientAcceptor(), new IPEndPoint(IPAddress.Any, 3725));
			server.Start();
			Console.ReadLine();
		}
	}
}