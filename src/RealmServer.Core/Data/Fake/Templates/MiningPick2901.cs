using System;
using Hazzik.Items;

namespace Hazzik.Data.Fake.Templates {
	public class MiningPick2901 : ItemTemplate
	{
		public MiningPick2901() {
			Id = 2901;
			ObjectClass = 2;
			SubClass = 14;
			Name = "Mining Pick";
			DisplayId = 6568;
			Quality = 1;
			BuyPrice = 81;
			SellPrice = 16;
			InventoryType = InventoryTypes.WeaponMainHand;
			Level = 4;
			RequiredLevel = 1;
			//Stackable = 1;
			SetDamage(2F, 4F, ResistanceTypes.Armor);
			AttackTime = 2000;
			Description = "Miners need a mining pick for digging.";
			Material = 1;
			TotemCategory = 11;
			BagFamily = 11;
		}
	}
}