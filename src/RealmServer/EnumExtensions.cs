using System;
using Hazzik.Objects;

namespace Hazzik {
	public static class EnumExtensions {
		public static bool Has(this UpdateFlags self, UpdateFlags flag) {
			return (self & flag) != 0;
		}
	}
}