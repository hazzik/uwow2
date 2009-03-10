using System;

namespace Hazzik.Objects {
	[Flags]
	public enum StateFlag {
		None = 0,
		AlwaysStand = 0x1,
		Sneaking = 0x2,
		UnTrackable = 0x4,
	}
}