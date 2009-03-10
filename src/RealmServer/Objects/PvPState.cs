using System;

namespace Hazzik.Objects {
	[Flags]
	public enum PvPState {
		PVP = 0x1,
		FFAPVP = 0x4,
		InPvPSanctuary = 0x8,
	}
}