using System;
using Hazzik.Objects;

namespace Hazzik.Items {
	public interface IEquipmentInventory : IInventory {
		Item AutoEquip(Item item);
	}
}