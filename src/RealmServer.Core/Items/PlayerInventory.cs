using System;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class PlayerInventory : Inventory, IEquipmentInventory {
		public PlayerInventory(IContainer container, uint slotsCount)
			: base(container, slotsCount) {
		}

		#region IEquipmentInventory Members

		public override int FindFreeSlot() {
			for(InventorySlot i = InventorySlot.BackpackStart; i < InventorySlot.BackpackEnd; i++) {
				if(null == this[(int)i]) {
					return (int)i;
				}
			}
			return -1;
		}

		public void AutoEquip(Item item) {
			var equipSlot = (EquipmentSlot)FindFreeSlot(item.Template.CanBeEquipedIn.Cast<int>());
			if(equipSlot != EquipmentSlot.None) {
				this[(int)equipSlot] = item;
			}
		}

		#endregion
	}
}