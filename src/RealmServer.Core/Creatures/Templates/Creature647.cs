using System;
using Hazzik.Creatures;

namespace Hazzik.Objects.Templates {
	public class Creature647 : CreatureTemplate {
		public Creature647() {
			Id = 647;
			//Level = 20;
			Name = "Captain Greenskin";
			DisplayId = 7113;
			//AttackSpeed = 2500;
			//CombatReach = 2.25f;
			//BoundingRadius = 0.459f;
			//Armor = MobArmorHP.GetMobArmor(Level + Rnd.PlusAR());
			//ScaleX = 1.0f;
			//Speed0 = 3f;
			//Speed1 = 3f;
			//Speed2 = 6.5f;
			//ResistArcane = 0;
			//ResistFire = 0;
			//ResistFrost = 0;
			//ResistHoly = 0;
			//ResistNature = 0;
			//ResistShadow = 0;
			Rank = (CreatureRank)1;
			//BaseHealth = 2900;
			//SetDamage((Rnd.ElHumMinKof() * AttackSpeed / 1000f) * (Level * (Elite + 1f)), (Rnd.ElHumMaxKof() * AttackSpeed / 1000f) * (Level * (Elite + 1f)));
			//BaseMana = Level * 70;
			Flags = (CreatureFlags)0x081000;
			Type = (CreatureType)7;
			//Faction = FactionTemplates.Monster;
			//AIEngine = new AgressiveAI(this);
			//LearnSpell(11608, SpellsTypes.Curse);
			//LearnSpell(5208, SpellsTypes.Curse);
			//Equip(new VirtualItem(7459, (InventoryTypes)13, 2, 14, 2, 2, 0, 0, 0));
		}
	}
}