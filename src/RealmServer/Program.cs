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
			ObjectFactory.Configure(config => {
			                        	config.ForRequestedType<IAccountRepository>().AddConcreteType<NHAccountRepository>();
			                        	config.ForRequestedType<IPlayerRepository>().AddConcreteType<NHPlayerRepository>();
			                        	config.ForRequestedType<IGameObjectTemplateRepository>().AddConcreteType<NHGameObjectTemplateRepository>();
			                        	config.ForRequestedType<ICreatureTemplateRepository>().AddConcreteType<FakeCreatureTemplateRepository>();
			                        	config.ForRequestedType<IItemTemplateRepository>().AddConcreteType<FakeItemTemplateRepository>();
			                        	config.ForRequestedType<INpcTextRepository>().AddConcreteType<FakeNpcTextRepository>();
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