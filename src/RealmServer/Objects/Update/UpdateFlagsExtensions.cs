using System;

namespace Hazzik.Objects.Update {
	internal static class UpdateFlagsExtensions {
		public static bool Has(this UpdateFlags self, UpdateFlags flag) {
			return (self & flag) != 0;
		}
	}
}