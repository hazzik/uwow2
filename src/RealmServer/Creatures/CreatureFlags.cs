using System;

namespace Hazzik.Creatures {
	[Flags]
	public enum CreatureFlags {
		Tamable = 0x1,
		SpiritHealer = 0x2,
		CanGatherHerbs = 0x100,
		CanMine = 0x200,
		Flag0x400 = 0x400,
		Flag0x4000 = 0x4000,
		CanSalvage = 0x8000,
		CanWalk = 0x00040000,
		CanSwim = 0x10000000,
	}
}