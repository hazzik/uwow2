using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class EquipmentInventory : InventoryWrapper {
		public EquipmentInventory(IContainer player)
			: base(player.Inventory, 0, 22) {
		}

		public override void AutoAdd(Item item) {
			var slot = FindFreeSlot(item.Template.CanBeEquipedIn.Cast<int>());
			if(slot != -1) {
				this[slot] = item;
			}
		}

		private int FindFreeSlot(IEnumerable<int> slots) {
			foreach(int slot in slots) {
				if(this[slot] == null) {
					return slot;
				}
			}
			return -1;
		}
	}
}