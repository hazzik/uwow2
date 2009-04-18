using Hazzik.Objects;

namespace Hazzik.Items {
	public class BankInventory : InventoryWrapper {
		public BankInventory(IContainer player)
			: base(player.Inventory, 39, 28) {
		}
	}
}