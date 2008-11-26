using System;
using System.Reflection;
using Hazzik.Net;

namespace Hazzik {
	internal class Program {
		private static void Main(string[] args) {
			var server = new WorldServer();
			server.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			server.Handler.Load();
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