using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class BackPackInventory : InventoryWrapper {
		public BackPackInventory(IContainer player)
			: base(player.Inventory, 23, 16) {
		}

		public override int FindFreeSlot() {
			var slot = base.FindFreeSlot();
			if(slot == -1) {
				for(var i = InventorySlot.Bag1; i <= InventorySlot.BagLast; i++) {
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