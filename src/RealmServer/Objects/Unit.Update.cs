using System;
using Hazzik.Creatures;
using Hazzik.Map;

namespace Hazzik.Objects {
	public partial class Unit {
		#region UNIT_FIELD_CHARM

		//UNIT_FIELD_CHARM : type = Long, size = 2, flag = Public
		public virtual UInt64 CharmGuid { get; set; }

		#endregion

		#region UNIT_FIELD_SUMMON

		//UNIT_FIELD_SUMMON : type = Long, size = 2, flag = Public
		public virtual UInt64 SummonGuid { get; set; }

		#endregion

		#region UNIT_FIELD_CRITTER

		//UNIT_FIELD_CRITTER : type = Long, size = 2, flag = Private
		public virtual UInt64 CritterGuid { get; set; }

		#endregion

		#region UNIT_FIELD_CHARMEDBY

		//UNIT_FIELD_CHARMEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 CharmedByGuid { get; set; }

		#endregion

		#region UNIT_FIELD_SUMMONEDBY

		//UNIT_FIELD_SUMMONEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 SummonedByGuid { get; set; }

		#endregion

		#region UNIT_FIELD_CREATEDBY

		//UNIT_FIELD_CREATEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 CreatedByGuid { get; set; }

		#endregion

		#region UNIT_FIELD_TARGET

		//UNIT_FIELD_TARGET : type = Long, size = 2, flag = Public
		public virtual UInt64 TargetGuid { get; set; }

		#endregion

		#region UNIT_FIELD_CHANNEL_OBJECT

		//UNIT_FIELD_CHANNEL_OBJECT : type = Long, size = 2, flag = Public
		public virtual UInt64 ChannelObjectGuid { get; set; }

		#endregion

		#region UNIT_FIELD_BYTES_0

		//UNIT_FIELD_BYTES_0 : type = Bytes, size = 1, flag = Public

		public virtual Races Race { get; set; }

		public virtual Classes Classe { get; set; }

		public virtual GenderType Gender { get; set; }

		public virtual PowerType PowerType { get; set; }

		#endregion

		#region UNIT_FIELD_HEALTH

		//UNIT_FIELD_HEALTH : type = Int, size = 1, flag = Public
		public virtual UInt32 Health { get; set; }

		#endregion

		#region UNIT_FIELD_POWER

		//UNIT_FIELD_POWER1 : type = Int, size = 1, flag = Public * 7
		private readonly uint[] _power = new uint[7];

		public virtual UInt32 Power {
			get { return _power[(int)PowerType]; }
			set {
				if(value < 0) {
					value = 0;
				}
				if(value > MaxPower) {
					value = MaxPower;
				}
				if(value == Power) {
					return;
				}
				_power[(int)PowerType] = value;
				ObjectManager.SendNearExceptMe(this, GetPowerUpdatePkt(value));
			}
		}

		#endregion

		#region UNIT_FIELD_MAXHEALTH

		//UNIT_FIELD_MAXHEALTH : type = Int, size = 1, flag = Public
		public virtual UInt32 MaxHealth { get; set; }

		#endregion

		#region UNIT_FIELD_MAXPOWER

		//UNIT_FIELD_MAXPOWER1 : type = Int, size = 1, flag = Public * 7
		private readonly uint[] _maxPower = new uint[7];

		public virtual UInt32 MaxPower {
			get { return _maxPower[(int)PowerType]; }
			set { _maxPower[(int)PowerType] = value; }
		}

		#endregion

		#region UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER

		//UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER : type = Single, size = 7, flag = Private, Owner
		public virtual Single PowerRegenFlatModifier { get; set; }

		#endregion

		#region  UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER

		//UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER : type = Single, size = 7, flag = Private, Owner
		public virtual Single PowerRegenInterruptedFlatModifier { get; set; }

		#endregion

		#region UNIT_FIELD_LEVEL

		//UNIT_FIELD_LEVEL : type = Int, size = 1, flag = Public
		public virtual UInt32 Level { get; set; }

		#endregion

		#region UNIT_FIELD_FACTIONTEMPLATE

		//UNIT_FIELD_FACTIONTEMPLATE : type = Int, size = 1, flag = Public
		public virtual UInt32 FactionTemplate { get; set; }

		#endregion

		#region UNIT_FIELD_FLAGS

		//UNIT_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags { get; set; }

		#endregion

		#region UNIT_FIELD_FLAGS_2

		//UNIT_FIELD_FLAGS_2 : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags2 { get; set; }

		#endregion

		#region UNIT_FIELD_AURASTATE

		//UNIT_FIELD_AURASTATE : type = Int, size = 1, flag = Public
		public virtual UInt32 AuraState { get; set; }

		#endregion

		#region UNIT_FIELD_RANGEDATTACKTIME

		//UNIT_FIELD_RANGEDATTACKTIME : type = Int, size = 1, flag = Private
		public virtual UInt32 RangedAttackTime { get; set; }

		#endregion

		#region UNIT_FIELD_BOUNDINGRADIUS

		//UNIT_FIELD_BOUNDINGRADIUS : type = Single, size = 1, flag = Public
		public virtual Single BoundingRadius { get; set; }

		#endregion

		#region UNIT_FIELD_COMBATREACH

		//UNIT_FIELD_COMBATREACH : type = Single, size = 1, flag = Public
		public virtual Single CombatReach { get; set; }

		#endregion

		#region UNIT_FIELD_DISPLAYID

		//UNIT_FIELD_DISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId { get; set; }

		#endregion

		#region UNIT_FIELD_NATIVEDISPLAYID

		//UNIT_FIELD_NATIVEDISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 NativeDisplayId { get; set; }

		#endregion

		#region UNIT_FIELD_MOUNTDISPLAYID

		//UNIT_FIELD_MOUNTDISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 MountDisplayId { get; set; }

		#endregion

		#region UNIT_FIELD_MINDAMAGE

		//UNIT_FIELD_MINDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MinDamage { get; set; }

		#endregion

		#region UNIT_FIELD_MAXDAMAGE

		//UNIT_FIELD_MAXDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MaxDamage { get; set; }

		#endregion

		#region UNIT_FIELD_MINOFFHANDDAMAGE

		//UNIT_FIELD_MINOFFHANDDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MinOffHandDamage { get; set; }

		#endregion

		#region UNIT_FIELD_MAXOFFHANDDAMAGE

		//UNIT_FIELD_MAXOFFHANDDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MaxOffHandDamage { get; set; }

		#endregion

		#region UNIT_FIELD_BYTES_1

		//UNIT_FIELD_BYTES_1 : type = Bytes, size = 1, flag = Public

		public virtual StandStates StandState { get; set; }

		public virtual byte PetTalentPoints { get; set; }

		public virtual StateFlag StateFlags { get; set; }

		public virtual byte UnitBytes1_3 { get; set; }

		#endregion

		#region UNIT_FIELD_PETNUMBER

		//UNIT_FIELD_PETNUMBER : type = Int, size = 1, flag = Public
		public virtual UInt32 PetNumber { get; set; }

		#endregion

		#region UNIT_FIELD_PET_NAME_TIMESTAMP

		//UNIT_FIELD_PET_NAME_TIMESTAMP : type = Int, size = 1, flag = Public
		public virtual UInt32 PetNameTimestamp { get; set; }

		#endregion

		#region UNIT_FIELD_PETEXPERIENCE

		//UNIT_FIELD_PETEXPERIENCE : type = Int, size = 1, flag = Owner
		public virtual UInt32 PetXp { get; set; }

		#endregion

		#region UNIT_FIELD_PETNEXTLEVELEXP

		//UNIT_FIELD_PETNEXTLEVELEXP : type = Int, size = 1, flag = Owner
		public virtual UInt32 PetNextLevelXp { get; set; }

		#endregion

		#region UNIT_DYNAMIC_FLAGS

		//UNIT_DYNAMIC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual UInt32 DynamicFlags { get; set; }

		#endregion

		#region UNIT_CHANNEL_SPELL

		//UNIT_CHANNEL_SPELL : type = Int, size = 1, flag = Public
		public virtual UInt32 ChannelSpell { get; set; }

		#endregion

		#region UNIT_MOD_CAST_SPEED

		//UNIT_MOD_CAST_SPEED : type = Single, size = 1, flag = Public
		public virtual Single ModCastSpeed { get; set; }

		#endregion

		#region UNIT_CREATED_BY_SPELL

		//UNIT_CREATED_BY_SPELL : type = Int, size = 1, flag = Public
		public virtual UInt32 CreatedBySpell { get; set; }

		#endregion

		#region UNIT_NPC_FLAGS

		//UNIT_NPC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual NpcFlags NpcFlags { get; set; }

		#endregion

		#region UNIT_NPC_EMOTESTATE

		//UNIT_NPC_EMOTESTATE : type = Int, size = 1, flag = Public
		public virtual UInt32 NpcEmoteState { get; set; }

		#endregion

		#region UNIT_FIELD_STAT0

		//UNIT_FIELD_STAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat0 { get; set; }

		#endregion

		#region UNIT_FIELD_STAT1

		//UNIT_FIELD_STAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat1 { get; set; }

		#endregion

		#region UNIT_FIELD_STAT2

		//UNIT_FIELD_STAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat2 { get; set; }

		#endregion

		#region UNIT_FIELD_STAT3

		//UNIT_FIELD_STAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat3 { get; set; }

		#endregion

		#region UNIT_FIELD_STAT4

		//UNIT_FIELD_STAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat4 { get; set; }

		#endregion

		#region UNIT_FIELD_POSSTAT0

		//UNIT_FIELD_POSSTAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat0 { get; set; }

		#endregion

		#region UNIT_FIELD_POSSTAT1

		//UNIT_FIELD_POSSTAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat1 { get; set; }

		#endregion

		#region UNIT_FIELD_POSSTAT2

		//UNIT_FIELD_POSSTAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat2 { get; set; }

		#endregion

		#region UNIT_FIELD_POSSTAT3

		//UNIT_FIELD_POSSTAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat3 { get; set; }

		#endregion

		#region UNIT_FIELD_POSSTAT4

		//UNIT_FIELD_POSSTAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat4 { get; set; }

		#endregion

		#region UNIT_FIELD_NEGSTAT0

		//UNIT_FIELD_NEGSTAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat0 { get; set; }

		#endregion

		#region UNIT_FIELD_NEGSTAT1

		//UNIT_FIELD_NEGSTAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat1 { get; set; }

		#endregion

		#region UNIT_FIELD_NEGSTAT2

		//UNIT_FIELD_NEGSTAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat2 { get; set; }

		#endregion

		#region UNIT_FIELD_NEGSTAT3

		//UNIT_FIELD_NEGSTAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat3 { get; set; }

		#endregion

		#region UNIT_FIELD_NEGSTAT4

		//UNIT_FIELD_NEGSTAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat4 { get; set; }

		#endregion

		#region UNIT_FIELD_BASE_MANA

		//UNIT_FIELD_BASE_MANA : type = Int, size = 1, flag = Public
		public virtual UInt32 BaseMana { get; set; }

		#endregion

		#region UNIT_FIELD_BASE_HEALTH

		//UNIT_FIELD_BASE_HEALTH : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 BaseHealth { get; set; }

		#endregion

		#region UNIT_FIELD_BYTES_2

		//UNIT_FIELD_BYTES_2 : type = Bytes, size = 1, flag = Public
		public SheathType Sheath { get; set; }

		public PvPState PvpState { get; set; }

		public PetState PetState { get; set; }

		public ShapeShiftForm ShapeShiftForm { get; set; }

		#endregion

		#region UNIT_FIELD_ATTACK_POWER

		//UNIT_FIELD_ATTACK_POWER : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 AttackPower { get; set; }

		#endregion

		#region UNIT_FIELD_ATTACK_POWER_MODS

		//UNIT_FIELD_ATTACK_POWER_MODS : type = Shorts, size = 1, flag = Private, Owner
		public virtual UInt32 AttackPowerMods { get; set; }

		#endregion

		#region UNIT_FIELD_ATTACK_POWER_MULTIPLIER

		//UNIT_FIELD_ATTACK_POWER_MULTIPLIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single AttackPowerMultiplier { get; set; }

		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER

		//UNIT_FIELD_RANGED_ATTACK_POWER : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 RangedAttackPower { get; set; }

		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER_MODS

		//UNIT_FIELD_RANGED_ATTACK_POWER_MODS : type = Shorts, size = 1, flag = Private, Owner
		public virtual UInt32 RangedAttackPowerMods { get; set; }

		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER

		//UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single RangedAttackPowerMultiplier { get; set; }

		#endregion

		#region UNIT_FIELD_MINRANGEDDAMAGE

		//UNIT_FIELD_MINRANGEDDAMAGE : type = Single, size = 1, flag = Private, Owner
		public virtual Single MinRangedDamage { get; set; }

		#endregion

		#region UNIT_FIELD_MAXRANGEDDAMAGE

		//UNIT_FIELD_MAXRANGEDDAMAGE : type = Single, size = 1, flag = Private, Owner
		public virtual Single MaxRangedDamage { get; set; }

		#endregion

		#region UNIT_FIELD_POWER_COST_MODIFIER

		//UNIT_FIELD_POWER_COST_MODIFIER : type = Int, size = 7, flag = Private, Owner
		public virtual UInt32 PowerCostModifier { get; set; }

		#endregion

		#region UNIT_FIELD_POWER_COST_MULTIPLIER

		//UNIT_FIELD_POWER_COST_MULTIPLIER : type = Single, size = 7, flag = Private, Owner
		public virtual Single PowerCostMultiplier { get; set; }

		#endregion

		#region UNIT_FIELD_MAXHEALTHMODIFIER

		//UNIT_FIELD_MAXHEALTHMODIFIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single MaxHealthModifier { get; set; }

		#endregion

		#region UNIT_FIELD_HOVERHEIGHT

		//UNIT_FIELD_HOVERHEIGHT : type = Single, size = 1, flag = Public
		public virtual Single HoverHeight { get; set; }

		#endregion

		//UNIT_VIRTUAL_ITEM_SLOT_ID : type = Int, size = 3, flag = Public

		//UNIT_FIELD_BASEATTACKTIME : type = Int, size = 2, flag = Public

		//UNIT_FIELD_RESISTANCES : type = Int, size = 7, flag = Private, Owner, PartyLeader

		//UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE : type = Int, size = 7, flag = Private, Owner

		//UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE : type = Int, size = 7, flag = Private, Owner

		//UNIT_FIELD_PADDING : type = Int, size = 1, flag = None
	}
}