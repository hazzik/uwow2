using System;
using System.Collections.Generic;
using System.Reflection;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	internal class Program {
		public static IList<Player> AllConnected = new List<Player>();

		private static void Main(string[] args) {
			var server = new WorldServer();
			WorldServer.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			WorldServer.Handler.Load();
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