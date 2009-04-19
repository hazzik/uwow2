using System;

namespace Hazzik.Items.Inventories {
	public interface IBankBagsInventory : IInventory {
		void BuySlot();
	}
}