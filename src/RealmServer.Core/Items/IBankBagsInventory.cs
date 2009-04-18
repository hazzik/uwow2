using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public interface IBankBagsInventory : IInventory {
		void BuySlot();
	}
}