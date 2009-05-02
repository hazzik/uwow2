using System;
using Hazzik.Items;

namespace Hazzik.Data.Fake.Templates {
	public class FelIronShells23772 : ItemTemplate {
		public FelIronShells23772() {
			Id = 23772;
			ObjectClass = 6;
			SubClass = 3;
			Name = "Fel Iron Shells";
			DisplayId = 40542;
			Quality = 2;
			BuyPrice = 8000;
			SellPrice = 20;
			InventoryType = InventoryTypes.Ammo;
			Level = 97;
			RequiredLevel = 57;
			MaxAmount = 200;
			SetDamage(26F, 26F, ResistanceTypes.Armor);
			AttackTime = 3000;
			Material = 2;
			TotemCategory = 2;
		}
	}
}