using System;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class BankInventory : InventoryWrapper {
		private readonly Player _player;

		public BankInventory(Player player)
			: base(player.Inventory, 39, 28) {
			_player = player;
		}

		public override int FindFreeSlot() {
			int slot = base.FindFreeSlot();
			if(slot == -1) {
				foreach(IContainer bag in _player.BankBags) {
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