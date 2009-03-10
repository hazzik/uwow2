using System;

namespace Hazzik.Objects {
	[Flags]
	public enum ObjectTypes : uint {
		None = 0,
		Object = 0x1,
		Item = 0x2,
		Container = 0x4,
		Unit = 0x8,
		Player = 0x10,
		GameObject = 0x20,
		Attackable = 0x28,
		DynamicObject = 0x40,
		Corpse = 0x80,
		AIGroup = 0x100,
		AreaTrigger = 0x200,
		All = 0xFFFF,
	}
}