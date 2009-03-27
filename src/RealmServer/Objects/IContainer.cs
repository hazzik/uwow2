using System;

namespace Hazzik.Objects {
	public interface IContainer {
		ulong Guid { get; }
		WorldObject Owner { get; }
		IInventory Inventory { get; }
	}
}