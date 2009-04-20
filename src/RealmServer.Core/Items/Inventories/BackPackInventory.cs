using System;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class BackPackInventory : InventoryWrapper {
		public BackPackInventory(Player player)
			: base(player.Inventory, 23, 16) {
		}

		public override bool AutoAdd(Item item) {
			if(base.AutoAdd(item)) {
				return true;
			}
			for(InventorySlot i = InventorySlot.Bag1; i <= InventorySlot.BagLast; i++) {
				var bag = this[(int)i] as IContainer;
				if(bag != null && bag.Inventory.AutoAdd(item)) {
					return true;
				}
			}
			return false;
		}
	}
}