using System;
using System.Net;
using System.Reflection;
using Hazzik.Net;
using Hazzik.RealmServer.PacketDispatchers;

namespace Hazzik {
	internal static class App {
		private static void Main() {
			IoC.Initialize(new DependencyResolver());

			var factory = new AttributesPacketDispatcherFactory();
			foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				factory.AddAssembly(assembly);
			}
			factory.Load();

			WorldPacketProcessor.Factory = factory;
			var server = Server.Create("WORLD SERVER", new IPEndPoint(IPAddress.Any, 3725), new WorldClientFactory());
			server.Start();
			Console.ReadLine();
		}
	}
}