using System;
using System.Net;
using System.Reflection;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Data.Fake;
using Hazzik.Data.NH;
using Hazzik.Net;
using StructureMap;

namespace Hazzik {
	internal class Program {
		private static void Main(string[] args) {
			ObjectFactory.Configure(x => {
			                        	x.ForRequestedType<IAccountRepository>().AddConcreteType<NHAccountRepository>();
			                        	x.ForRequestedType<IPlayerRepository>().AddConcreteType<NHPlayerRepository>();
			                        	x.ForRequestedType<IGameObjectTemplateRepository>().AddConcreteType<NHGameObjectTemplateRepository>();
			                        	x.ForRequestedType<ICreatureTemplateRepository>().AddConcreteType<FakeCreatureTemplateRepository>();
			                        });

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