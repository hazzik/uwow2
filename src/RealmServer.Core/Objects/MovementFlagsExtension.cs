using System;

namespace Hazzik.Objects {
	public static class MovementFlagsExtension {
		public static bool Has(this MovementFlags source, MovementFlags flag) {
			return (source & flag) != 0;
		}
		public static bool Has(this MovementFlags2 source, MovementFlags2 flag) {
			return (source & flag) != 0;
		}
	}
}