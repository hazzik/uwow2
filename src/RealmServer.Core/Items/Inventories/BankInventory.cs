using System;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class BankInventory : InventoryWrapper {
		private readonly Player _player;

		public BankInventory(Player player)
			: base(player.Inventory, 39, 28) {
			_player = player;
		}

		public override bool AutoAdd(Item item) {
			if(base.AutoAdd(item)) {
				return true;
			}
			foreach(IContainer bag in _player.BankBags) {
				if(bag != null && bag.Inventory.AutoAdd(item)) {
					return true;
				}
			}
			return false;
		}
	}
}