using System;

namespace Hazzik.Objects {
	[Flags]
	public enum MovementFlags {
		None = 0x00000000,
		Forward = 0x00000001,
		Backward = 0x00000002,
		StrafeLeft = 0x00000004,
		StrafeRight = 0x00000008,
		Left = 0x00000010,
		RIGHT = 0x00000020,
		PitchUp = 0x00000040,
		PitchDown = 0x00000080,
		Walk = 0x00000100,
		OnTransport = 0x00000200,
		Unk1 = 0x00000400,
		FlyUnk1 = 0x00000800,
		Jumping = 0x00001000,
		Unk2 = 0x00002000,
		Falling = 0x00004000,
		// 0x8000, 0x10000, 0x20000, 0x40000, 0x80000, 0x100000
		Swimming = 0x00200000, // appears with fly flag also
		FlyUp = 0x00400000,
		CanFly = 0x00800000,
		Flying = 0x01000000,
		Unk3 = 0x02000000,
		Spline = 0x04000000, // probably wrong name
		Spline2 = 0x08000000,
		WaterWalking = 0x10000000,
		SafeFall = 0x20000000, // active rogue safe fall spell (passive)
		Unk4 = 0x40000000,
	}
}