using System;

namespace Hazzik {
	public class ObjectGuid {
		private static ulong _lastGuid = 0;

		public static ulong NewGuid() {
			return ++_lastGuid;
		}
	}
}