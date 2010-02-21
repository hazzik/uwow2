using System;

namespace Hazzik {
	public static class ObjectGuid {
		private static ulong lastGuid;

		public static ulong NewGuid() {
			return ++lastGuid;
		}
	}
}