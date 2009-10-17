using System;

namespace Hazzik {
	public class ObjectGuid {
		private static ulong lastGuid;

		public static ulong NewGuid() {
			return ++lastGuid;
		}
	}
}