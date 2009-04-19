using System;

namespace Hazzik.Items.Inventories {
	public class BackPackInventory : InventoryWrapper {
		public BackPackInventory(IContainer player)
			: base(player.Inventory, 23, 16) {
		}

		public override int FindFreeSlot() {
			int slot = base.FindFreeSlot();
			if(slot == -1) {
				for(InventorySlot i = InventorySlot.Bag1; i <= InventorySlot.BagLast; i++) {
					var bag = this[(int)i] as IContainer;
					if(bag != null) {
						slot = ((ContainerInventory)bag.Inventory).FindFreeSlot();
						if(slot != -1) {
							return slot;
						}
					}
				}
			}
			return slot;
		}
	}
}