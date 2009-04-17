using System;

namespace Hazzik.Objects {
	[Flags]
	public enum CorpseDynamicFlags {
		None = 0,
		/// <summary>
		/// "Sparks" emerging from the Corpse
		/// </summary>
		PlayerLootable = 0x1
	}
}