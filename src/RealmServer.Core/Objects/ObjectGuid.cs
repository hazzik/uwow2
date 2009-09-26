using System;

namespace Hazzik {
	public class ObjectGuid {
		private static ulong _lastGuid;

		public static ulong NewGuid() {
			return ++_lastGuid;
		}
	}
}