using System;

namespace Hazzik.GameObjects {
	[Flags]
	public enum GameObjectDynamicFlags : ushort {
		Deactivated = 0x00,
		Activated = 0x01,
	}
}