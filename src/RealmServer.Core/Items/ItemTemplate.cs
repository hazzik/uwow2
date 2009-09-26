using System;

namespace Hazzik.Items {
	public class ItemTemplate {
		#region Nested Types

		#region Nested type: Bonus

		public struct Bonus {
			public int Type;
			public int Value;
		}

		#endregion

		#region Nested type: Damage

		public struct Damage {
			public float Max;
			public float Min;
			public int School;
		}

		#endregion

		#region Nested type: Socket

		public struct Socket {
			public int Color;
			public int Content; // bonus?
		}

		#endregion

		#region Nested type: Spell

		public struct Spell {
			public int CategoryCooldown;
			public int CategoryId;
			public int Charges;
			public int Cooldown;
			public int Id;
			public int Trigger;
		}

		#endregion

		#endregion

		#region Fields - Public

		public int ArmorModifier;
		public int AttackTime; // 
		public int BagFamily;
		public int BlockValue;
		public int BondType;
		public Bonus[] bonuses = new Bonus[10];
		public int BuyPrice;
		public int ContainerSlots;
		public Damage[] damages = new Damage[2];
		public string Description = string.Empty;
		public int DisplayId;
		public int ExtendedCost;
		public int Faction;
		public int Flags;
		public int GemPropertiesId;
		public int Id;
		public InventoryTypes InventoryType;
		public int Level;
		public int LockId;
		public int MapId;
		public int Material;
		public int MaxAmount; // MaxStack
		public int MaxDurability;
		public string Name = string.Empty;
		public string Name2 = string.Empty;
		public string Name3 = string.Empty;
		public string Name4 = string.Empty;
		public int ObjectClass;
		public int PaeCount;
		public int PageMaterial;
		public int PageTextId;
		public int ProjectileType;
		public int Quality;
		public int QuestId;
		public int RandomPropertiesId;
		public int RandomSuffixId;
		public float RangeModifier;
		public int RequiredClassMask = -1;
		public int RequiredDisenchantingLevel = -1;
		public int RequiredFaction;
		public int RequiredFactionStanding;
		public int RequiredLevel;
		public int RequiredPvPMedal;
		public int RequiredPvPRank;
		public int RequiredRaceMask = -1;
		public int RequiredSkill; // RequiredSkill?
		public int RequiredSkillValue; // RequiredSkillLevel?
		public int RequiredSpell;
		public int[] Resistance = new int[7];
		public int SellPrice;
		public int SetId;
		public int SheathType;
		public int SocketBonusEnchantId;
		public Socket[] sockets = new Socket[3];
		public Spell[] spells = new Spell[5];
		public int SubClass;
		public int TotemCategory;
		public int UniqueCount; // wtf?
		public int Unk1 = -1;
		public int ZoneId;

		#endregion

		#region Accessors

		public bool IsQuestItem {
			//get { return (SubClass == 0 && (ObjectClass == 12 || ObjectClass == 15)); }
			get { return (SubClass == 0 && ObjectClass == 12); }
		}

		public bool IsSoulBag {
			get { return (ObjectClass == 1 && SubClass == 1); }
		}

		public bool IsHerbBag {
			get { return (ObjectClass == 1 && SubClass == 2); }
		}

		public bool IsEnchantingBag {
			get { return (ObjectClass == 1 && SubClass == 3); }
		}

		public bool IsEngineeringBags {
			get { return (ObjectClass == 1 && SubClass == 4); }
		}

		public bool IsGemBag {
			get { return (ObjectClass == 1 && SubClass == 5); }
		}

		public bool IsMiningBag {
			get { return (ObjectClass == 1 && SubClass == 6); }
		}

		public bool IsAmmoPouch {
			get { return (ObjectClass == 11 && SubClass == 3); }
		}

		public bool IsArrow {
			get { return (ObjectClass == 6 && SubClass == 2); }
		}

		public bool IsBullet {
			get { return (ObjectClass == 6 && SubClass == 3); }
		}

		public bool IsContainer {
			get { return (ContainerSlots > 0); }
		}

		public bool IsQuiver {
			get { return (ObjectClass == 11 && SubClass == 2); }
		}

		public bool IsShield {
			get { return (ObjectClass == 4 && SubClass == 6); }
		}

		public bool IsKey {
			get { return (ObjectClass == 13); }
		}

		public bool IsWeapon {
			get { return (ObjectClass == 2); }
		}

		public int[] CanBeEquipedIn {
			get {
				switch(InventoryType) {
				case InventoryTypes.None:
					return new int[0];
				case InventoryTypes.Head:
					return new[] { (int)EquipmentSlot.Head };
				case InventoryTypes.Neck:
					return new[] { (int)EquipmentSlot.Neck };
				case InventoryTypes.Shoulder:
					return new[] { (int)EquipmentSlot.Shoulders };
				case InventoryTypes.Shirt:
					return new[] { (int)EquipmentSlot.Shirt };
				case InventoryTypes.Chest:
					return new[] { (int)EquipmentSlot.Chest };
				case InventoryTypes.Waist:
					return new[] { (int)EquipmentSlot.Waist };
				case InventoryTypes.Legs:
					return new[] { (int)EquipmentSlot.Legs };
				case InventoryTypes.Feet:
					return new[] { (int)EquipmentSlot.Feet };
				case InventoryTypes.Wrist:
					return new[] { (int)EquipmentSlot.Wrists };
				case InventoryTypes.Hands:
					return new[] { (int)EquipmentSlot.Hands };
				case InventoryTypes.Finger:
					return new[] { (int)EquipmentSlot.FingerLeft, (int)EquipmentSlot.FingerRight };
				case InventoryTypes.Trinket:
					return new[] { (int)EquipmentSlot.TrinketLeft, (int)EquipmentSlot.TrinketRight };
				case InventoryTypes.Weapon:
					return new[] { (int)EquipmentSlot.MainHand, (int)EquipmentSlot.OffHand };
				case InventoryTypes.Shield:
				case InventoryTypes.WeaponOffHand:
				case InventoryTypes.Holdable:
					return new[] { (int)EquipmentSlot.OffHand };
				case InventoryTypes.Ranged:
				case InventoryTypes.Thrown:
				case InventoryTypes.RangedRight:
				case InventoryTypes.Relic:
					return new[] { (int)EquipmentSlot.Ranged };
				case InventoryTypes.Back:
					return new[] { (int)EquipmentSlot.Back };
				case InventoryTypes.TwoHanded:
				case InventoryTypes.WeaponMainHand:
					return new[] { (int)EquipmentSlot.MainHand };
				case InventoryTypes.Bag:
				case InventoryTypes.Quiver:
					return new[] { (int)EquipmentSlot.Bag1, (int)EquipmentSlot.Bag2, (int)EquipmentSlot.Bag3, (int)EquipmentSlot.BagLast };
				case InventoryTypes.Tabard:
					return new[] { (int)EquipmentSlot.Tabard };
				case InventoryTypes.Robe:
					return new[] { (int)EquipmentSlot.Chest };
				case InventoryTypes.Ammo:
					return new[] { (int)EquipmentSlot.None };
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		#endregion

		#region Methods - Public

		private int b_i;
		private int d_i;
		private int s_i;

		public void SetBonus(ItemModType type, int amount) {
			if(b_i < bonuses.Length) {
				bonuses[b_i].Type = (int)type;
				bonuses[b_i].Value = amount;
				b_i++;
			}
		}

		public int GetBonus(ItemModType type) {
			var t = (int)type;
			foreach(Bonus b in bonuses) {
				if(b.Type == (int)type) {
					return b.Value;
				}
			}
			return 0;
		}

		public void SetDamage(float min, float max, ResistanceTypes res) {
			if(d_i < damages.Length) {
				damages[d_i].Min = min;
				damages[d_i].Max = max;
				damages[d_i].School = (int)res;
				d_i++;
			}
		}

		public void SetSpell(int id, int trigger, int charges, int cooldown, int category, int categorycooldown) {
			if(s_i < spells.Length) {
				spells[s_i].Id = id;
				spells[s_i].Trigger = trigger;
				spells[s_i].Charges = charges;
				spells[s_i].Cooldown = cooldown;
				spells[s_i].CategoryId = category;
				spells[s_i].CategoryCooldown = categorycooldown;
				s_i++;
			}
		}

		/*
		public int GetSkillId() {
			//object o = Item.skillIdAssociated[(int)(this.ObjectClass * 100 + this.SubClass)];
			if(o != null)
				return (int)o;
			return 0;
		}
		 */

		#endregion

		public bool IsBag {
			get { return ObjectClass == 11; }
		}
	}
}