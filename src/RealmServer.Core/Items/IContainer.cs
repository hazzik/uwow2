using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public interface IContainer {
		ulong Guid { get; }
		WorldObject Owner { get; }
		IInventory Inventory { get; }
	}
}