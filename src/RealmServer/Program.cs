using System;
using System.Net;
using System.Reflection;
using Hazzik.Net;

namespace Hazzik {
	internal class Program {
		private static void Main(string[] args) {
			ServiceLocator.Initialize(new StructureMapServiceLocator());
			var factory = new AttributesPacketDispatcherFactory();
			foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				factory.AddAssembly(assembly);
			}
			factory.Load();
			WorldPacketProcessor.Factory = factory;
			var server = new Server("WORLD SERVER", new WorldClientAcceptor(), new IPEndPoint(IPAddress.Any, 3725));
			server.Start();
			Console.ReadLine();
		}
	}
}