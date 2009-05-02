using System;
using Hazzik.Items;

namespace Hazzik.Data.Fake.Templates {
	public class LargeRedSack857 : ItemTemplate {
		public LargeRedSack857() {
			Id = 857;
			ObjectClass = 1;
			SubClass = 0;
			Name = "Large Red Sack";
			DisplayId = 4056;
			Quality = 1;
			BuyPrice = 10000;
			SellPrice = 2500;
			InventoryType = InventoryTypes.Bag;
			Level = 25;
			MaxAmount = 1;
			ContainerSlots = 10;
			Material = 8;
		}
	}
}