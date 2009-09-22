using System;

namespace Hazzik.PacketHandlers {
	public static class WorldServerHandlers {
		public static uint GetActualTime() {
			DateTime time = DateTime.Now;
			int year = time.Year - 2000;
			int month = time.Month - 1;
			int day = time.Day - 1;
			var dayOfWeek = (int)time.DayOfWeek;
			int hour = time.Hour;
			int minute = time.Minute;

			return (uint)(minute | (hour << 0x06) | (dayOfWeek << 0x0B) | (day << 0x0E) | (month << 0x14) | (year << 0x18));
		}
	}
}