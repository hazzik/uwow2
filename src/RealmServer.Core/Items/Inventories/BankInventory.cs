using System;

namespace Hazzik.Items.Inventories {
	public class BankInventory : InventoryWrapper {
		public BankInventory(IContainer player)
			: base(player.Inventory, 39, 28) {
		}
	}
}