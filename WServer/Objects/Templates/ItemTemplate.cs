using System;
using System.Collections.Generic;
using System.Text;
using UWoW.Net;

namespace UWoW
{
	public class ItemTemplate
	{
		#region Nested Types

		public struct bonus
		{
			public int type;
			public int value;
		}
		public struct damage
		{
			public float min;
			public float max;
			public int school;
		}
		public struct spell
		{
			public int id;
			public int trigger;
			public int charges;
			public int cooldown;
			public int category;
			public int categorycooldown;
		}
		public struct socket
		{
			public int type;
			public int unk; // bonus?
		}

		#endregion

		#region ctors

		//private ItemTemplate() { }

		#endregion

		#region fields

		public int Id;
		public int ObjectClass;
		public int SubClass;
		public int Unk1 = -1;
		public string Name = string.Empty;
		public string Name2 = string.Empty;
		public string Name3 = string.Empty;
		public string Name4 = string.Empty;
		public int Model;
		public int Quality;
		public int Flags;
		public int BuyPrice;
		public int SellPrice;
		public int InventoryType;
		public int AvailableClasses;
		public int AvailableRaces;
		public int Level;
		public int RequiredLevel;
		public int Skill; // RequiredSkill?
		public int SkillRank; // RequiredSkillLevel?
		public int RequiredSpell;
		public int RequiredHonorRank;
		public int RequiredProtectorRace;
		public int RequiredFaction;
		public int RequiredFactionStanding;
		public int UniqueCount; // wtf?
		public int Stackable; // MaxStack
		public int ContainerSlots;
		public bonus[] bonuses = new bonus[10];
		public damage[] damages = new damage[5];
		public int[] Resistance = new int[7];
		public int Delay; // 
		public int AmmoType;
		public float RangeModifier;
		public spell[] spells = new spell[5];
		public int Bonding;
		public string Description = string.Empty;
		public int TextId;
		public int TextLanguage;
		public int PageMaterial;
		public int StartQuest;
		public int LockId;
		public int Material;
		public int Sheath;
		public int Extra;
		public int Unk2;
		public int Block;
		public int Sets;
		public int Durability;
		public int ZoneIdProps; // Map
		public int TotemCategory;
		public int BagFamily;
		public socket[] sockets = new socket[3];
		public int BonusSpellItemEnch;
		public int GemProperties;
		public int ExtendedCost;
		public int RequiredDisenchantSkill = -1;

		#endregion

		#region methods

		private ServerPacket pkg;
		public ServerPacket GetResponce()
		{
			if (null == pkg)
			{
				pkg = new ServerPacket(OpCodes.SMSG_ITEM_QUERY_SINGLE_RESPONSE);
				pkg.Write(Id);
				pkg.Write(ObjectClass);
				pkg.Write(SubClass);
				pkg.Write(Unk1);
				pkg.Write(Name);
				pkg.Write(Name2);
				pkg.Write(Name3);
				pkg.Write(Name4);
				pkg.Write(Model);
				pkg.Write(Quality);
				pkg.Write(Flags);
				pkg.Write(BuyPrice);
				pkg.Write(SellPrice);
				pkg.Write(InventoryType);
				pkg.Write(AvailableClasses);
				pkg.Write(AvailableRaces);
				pkg.Write(Level);
				pkg.Write(RequiredLevel);
				pkg.Write(Skill);
				pkg.Write(SkillRank);
				pkg.Write(RequiredSpell);
				pkg.Write(RequiredHonorRank);
				pkg.Write(RequiredProtectorRace);
				pkg.Write(RequiredFaction);
				pkg.Write(RequiredFactionStanding);
				pkg.Write(UniqueCount);
				pkg.Write(Stackable);
				pkg.Write(ContainerSlots);

				for (int i = 0; i < 10; i++)
				{
					pkg.Write(bonuses[i].type);
					pkg.Write(bonuses[i].value);
				}

				for (int i = 0; i < 5; i++)
				{
					pkg.Write(damages[i].min);
					pkg.Write(damages[i].max);
					pkg.Write(damages[i].school);
				}

				for (int i = 0; i < 7; i++)
				{
					pkg.Write(Resistance[i]);
				}

				pkg.Write(Delay); // 
				pkg.Write(AmmoType);
				pkg.Write(RangeModifier);

				for (int i = 0; i < 5; i++)
				{
					pkg.Write(spells[i].id);
					pkg.Write(spells[i].trigger);
					pkg.Write(spells[i].charges);
					pkg.Write(spells[i].cooldown);
					pkg.Write(spells[i].category);
					pkg.Write(spells[i].categorycooldown);
				}

				pkg.Write(Bonding);
				pkg.Write(Description);
				pkg.Write(TextId);
				pkg.Write(TextLanguage);
				pkg.Write(PageMaterial);
				pkg.Write(StartQuest);
				pkg.Write(LockId);
				pkg.Write(Material);
				pkg.Write(Sheath);
				pkg.Write(Extra);
				pkg.Write(Unk2);
				pkg.Write(Block);
				pkg.Write(Sets);
				pkg.Write(Durability);
				pkg.Write(ZoneIdProps); // Map
				pkg.Write(TotemCategory);
				pkg.Write(BagFamily);

				for (int i = 0; i < 3; i++)
				{
					pkg.Write(sockets[i].type);
					pkg.Write(sockets[i].unk);
				}

				pkg.Write(BonusSpellItemEnch);
				pkg.Write(GemProperties);
				pkg.Write(ExtendedCost);
				pkg.Write(RequiredDisenchantSkill);
			}
			return pkg;
		}

		#endregion
	}
}
