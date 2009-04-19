using System;
using System.Collections.Generic;
using Hazzik.Objects;

namespace Hazzik.Items {
	public interface IInventory : IEnumerable<Item> {
		IContainer Container { get; }
		uint Slots { get; }
		Item this[int slot] { get; set; }
		int GetAmount(int id);
		void DestroyItem(int slot);
		void AutoAdd(Item item);
	}
}