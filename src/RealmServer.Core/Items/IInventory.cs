using System;
using System.Collections.Generic;

namespace Hazzik.Objects {
	public interface IInventory : IEnumerable<Item> {
		IContainer Container { get; }
		uint Slots { get; }
		Item this[int slot] { get; set; }
		int GetAmount(int id);
		void DestroyItem(int slot);
		int FindFreeSlot();
		int FindFreeSlot(IEnumerable<int> slots);
	}

	public interface IEquipmentInventory : IInventory {
		void AutoEquip(Item item);
	}
}