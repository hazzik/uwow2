using System;

namespace Hazzik {
	public static class DateTimeExtension {
		private static readonly DateTime deltaTime = new DateTime(1970, 1, 1);

		public static uint ToUnixTimestamp(this DateTime d) {
			return (uint)(new TimeSpan(DateTime.Now.Ticks - deltaTime.Ticks)).TotalSeconds;
		}

		public static DateTime ToDateTime(uint unixTimestamp) {
			return deltaTime.AddSeconds(unixTimestamp);
		}
	}
}