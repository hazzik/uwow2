using System;
using System.Net;
using System.Reflection;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik {
	public class Program {
		private static void Main(string[] args) {
			Handler = new PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute>();
			Handler.AddAssembly(Assembly.GetExecutingAssembly());
			Handler.Load();

			var server = new Server("AUTH SERVER", new AuthClientAcceptor(), new IPEndPoint(IPAddress.Any, 3724));
			server.Start();
			Console.ReadLine();
		}

		public static PacketHandler<PacketHandlerClassAttribute, AuthPacketHandlerAttribute> Handler { get; set; }
	}
}