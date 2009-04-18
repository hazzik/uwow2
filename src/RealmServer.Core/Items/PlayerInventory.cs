using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class PlayerInventory : Inventory {
		public PlayerInventory(IContainer container, uint slotsCount)
			: base(container, slotsCount) {
		}
	}
}