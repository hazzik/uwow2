using System;
using Hazzik.Items;

namespace Hazzik.Data.Fake.Templates {
	public class AuchenaiKey30633 : ItemTemplate {
		public AuchenaiKey30633() {
			Id = 30633;
			ObjectClass = 13;
			SubClass = 0;
			Name = "Auchenai Key";
			DisplayId = 22071;
			Quality = 1;
			BuyPrice = 100000;
			SellPrice = 25000;
			//RequiredFaction = (int)AbsoluteFaction.LowerCity;
			//RequiredFactionStanding = (int)ReputationRank.Revered;
			UniqueCount = 1;
			MaxAmount = 1;
			BondType = 1;
			Description = "Unlocks access to  Heroic mode for Auchindoun dungeons.";
			Material = -1;
			TotemCategory = 9;
		}
	}
}