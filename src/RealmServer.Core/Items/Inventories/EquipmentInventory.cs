using System;
using System.Collections.Generic;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class EquipmentInventory : InventoryWrapper, IEquipmentInventory {
		public EquipmentInventory(IContainer player)
			: base(player.Inventory, 0, 22) {
		}

		#region IEquipmentInventory Members

		public override bool AutoAdd(Item item) {
			int slot = FindFreeSlot(item.Template.CanBeEquipedIn);
			if(slot != -1) {
				this[slot] = item;
				return true;
			}
			return false;
		}

		public Item AutoEquip(Item item) {
			int slot = FindFreeSlot(item.Template);
			if(slot != -1) {
				Item old = this[slot];
				this[slot] = item;
				return old;
			}
			return item;
		}

		#endregion

		private int FindFreeSlot(ItemTemplate template) {
			int[] slots = template.CanBeEquipedIn;
			int slot = FindFreeSlot(slots);
			if(slot == -1 && !template.IsBag && slots.Length > 0) {
				return slots[0];
			}
			return slot;
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