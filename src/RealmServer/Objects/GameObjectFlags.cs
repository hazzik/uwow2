using System;

namespace Hazzik.Objects {
	[Flags]
	public enum GameObjectFlags : ushort {
		InUse = 0x01,
		Locked = 0x02,
		ConditionalInteraction = 0x04,
		Transport = 0x08,
		GOFlagLow_0x10 = 0x10,
		DoesNotDespawn = 0x20,
		Triggered = 0x40,

		GOFlagLow_0x80 = 0x80,
		GOFlagLow_0x100 = 0x100,
		GOFlagLow_0x200 = 0x200,
		GOFlagLow_0x400 = 0x400,
		GOFlagLow_0x800 = 0x800,
		GOFlagLow_0x1000 = 0x1000,
		GOFlagLow_0x2000 = 0x2000,
		GOFlagLow_0x4000 = 0x4000,
		GOFlagLow_0x8000 = 0x8000,
	}
}