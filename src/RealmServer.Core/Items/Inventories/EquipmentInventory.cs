using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class EquipmentInventory : InventoryWrapper, IEquipmentInventory {
		public EquipmentInventory(IContainer player)
			: base(player.Inventory, 0, 22) {
		}

		public override void AutoAdd(Item item) {
			var slot = FindFreeSlot(item.Template.CanBeEquipedIn.Cast<int>());
			if(slot != -1) {
				this[slot] = item;
			}
		}

		public Item AutoEquip(Item item) {
			var slot = FindFreeSlot(item.Template);
			if(slot != -1) {
				var old = this[slot];
				this[slot] = item;
				return old;
			}
			return item;
		}

		private int FindFreeSlot(ItemTemplate template) {
			var slots = template.CanBeEquipedIn;
			var slot = FindFreeSlot(slots);
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