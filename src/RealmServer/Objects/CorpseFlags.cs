using System;

namespace Hazzik.Objects {
	[Flags]
	public enum CorpseFlags {
		None = 0,
		IsClaimed = 0x1,
		Bones = 0x4,
	}
}