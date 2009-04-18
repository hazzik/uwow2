using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class ContainerInventory : Inventory {
		public ContainerInventory(IContainer container, uint slotsCount)
			: base(container, slotsCount) {
		}

		public override int FindFreeSlot() {
			for(var i = 0; i < Slots; i++) {
				if(null == this[i]) {
					return i;
				}
			}
			return -1;
		}
	}
}