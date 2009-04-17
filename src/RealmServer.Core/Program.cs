using System;
using System.Net;
using System.Reflection;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik {
	internal class Program {
		public static PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute> Handler { get; set; }

		private static void Main(string[] args) {
			Handler = new PacketHandler<PacketHandlerClassAttribute, WorldPacketHandlerAttribute>();
			Handler.AddAssembly(Assembly.GetExecutingAssembly());
			Handler.Load();
			var server = new Server("WORLD SERVER", new WorldClientAcceptor(), new IPEndPoint(IPAddress.Any, 3725));
			server.Start();
			Console.ReadLine();
		}

		public static uint GetActualTime() {
			var time = DateTime.Now;
			var year = time.Year - 2000;
			var month = time.Month - 1;
			var day = time.Day - 1;
			var dayOfWeek = (int)time.DayOfWeek;
			var hour = time.Hour;
			var minute = time.Minute;

			return (uint)(minute | (hour << 0x06) | (dayOfWeek << 0x0B) | (day << 0x0E) | (month << 0x14) | (year << 0x18));
		}
	}
}