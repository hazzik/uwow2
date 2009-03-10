using System;

namespace Hazzik.Objects {
	[Flags]
	public enum PetState {
		CanBeRenamed = 0x1,
		CanBeAbandoned = 0x2,
	}
}