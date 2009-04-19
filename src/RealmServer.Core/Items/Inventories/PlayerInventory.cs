using System;

namespace Hazzik.Items.Inventories {
	public class PlayerInventory : Inventory {
		public PlayerInventory(IContainer container, uint slotsCount)
			: base(container, slotsCount) {
		}
	}
}