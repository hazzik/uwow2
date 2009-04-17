using System;

namespace Hazzik.Objects {
	[Flags]
	public enum GameObjectDynamicFlags : ushort {
		Deactivated = 0x00,
		Activated = 0x01,
	}
}