using System;

namespace Hazzik.Objects.Update {
	[Flags]
	public enum UpdateFlags : ushort {
		None = 0x00,
		Self = 0x01,
		Transport = 0x02,
		TargetGuid = 0x04,
		LowGuid = 0x08,
		HighGuid = 0x10,
		Mobile = 0x20,
		HasPosition = 0x40,
		Unk1 = 0x80,
		Unk2 = 0x100,
		Unk3 = 0x200,
	}
}