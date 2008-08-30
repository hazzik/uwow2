using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik {
	public static class DataTimeExtension {
		private static DateTime deltaTime = new DateTime(1970, 1, 1);

		public static uint ToUnixTimestamp(this DateTime d) {
			return (uint)(new TimeSpan(DateTime.Now.Ticks - deltaTime.Ticks)).TotalSeconds;
		}

		public static DateTime ToDateTime(uint unixTimestamp) {
			return deltaTime.AddSeconds((double)unixTimestamp);
		}
	}
}
