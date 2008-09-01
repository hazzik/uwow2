using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Hazzik.Net;
using Hazzik.Attributes;

namespace Hazzik {
	class Program {
		static void Main(string[] args) {
			var server = new WorldServer();
			server.Handler.AddAssembly(Assembly.GetExecutingAssembly());
			server.Handler.Load();
			server.Start();
			Console.ReadLine();
		}

		public static uint GetActualTime() {
			DateTime time = DateTime.Now;
			int year = time.Year - 2000;
			int month = time.Month - 1;
			int day = time.Day - 1;
			int dayOfWeek = (int)time.DayOfWeek;
			int hour = time.Hour;
			int minute = time.Minute;
			//if(World.onGetActualTime != null)
			//   World.onGetActualTime(ref year, ref month, ref day, ref dayOfWeek, ref hour, ref minute);

			return (uint)(minute | (hour << 0x06) | (dayOfWeek << 0x0B) | (day << 0x0E) | (month << 0x14) | (year << 0x18));
		}
	}
}
