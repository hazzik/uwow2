using System;
using Hazzik.Map;

namespace Hazzik.Objects {
	public partial class Unit {
		#region UNIT_FIELD_CHARM
		//UNIT_FIELD_CHARM : type = Long, size = 2, flag = Public
		public virtual UInt64 CharmGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_CHARM); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_CHARM, value); }
		}
		#endregion

		#region UNIT_FIELD_SUMMON
		//UNIT_FIELD_SUMMON : type = Long, size = 2, flag = Public
		public virtual UInt64 SummonGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_SUMMON); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_SUMMON, value); }
		}
		#endregion

		#region UNIT_FIELD_CRITTER
		//UNIT_FIELD_CRITTER : type = Long, size = 2, flag = Private
		public virtual UInt64 CritterGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_CRITTER); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_CRITTER, value); }
		}
		#endregion

		#region UNIT_FIELD_CHARMEDBY
		//UNIT_FIELD_CHARMEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 CharmedByGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_CHARMEDBY); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_CHARMEDBY, value); }
		}
		#endregion

		#region UNIT_FIELD_SUMMONEDBY
		//UNIT_FIELD_SUMMONEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 SummonedByGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_SUMMONEDBY); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_SUMMONEDBY, value); }
		}
		#endregion

		#region UNIT_FIELD_CREATEDBY
		//UNIT_FIELD_CREATEDBY : type = Long, size = 2, flag = Public
		public virtual UInt64 CreatedByGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_CREATEDBY); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_CREATEDBY, value); }
		}
		#endregion

		#region UNIT_FIELD_TARGET
		//UNIT_FIELD_TARGET : type = Long, size = 2, flag = Public
		public virtual UInt64 TargetGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_TARGET); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_TARGET, value); }
		}
		#endregion

		#region UNIT_FIELD_CHANNEL_OBJECT
		//UNIT_FIELD_CHANNEL_OBJECT : type = Long, size = 2, flag = Public
		public virtual UInt64 ChannelObjectGuid {
			get { return GetUInt64(UpdateFields.UNIT_FIELD_CHANNEL_OBJECT); }
			set { SetUInt64(UpdateFields.UNIT_FIELD_CHANNEL_OBJECT, value); }
		}
		#endregion

		#region UNIT_FIELD_BYTES_0
		//UNIT_FIELD_BYTES_0 : type = Bytes, size = 1, flag = Public

		public virtual Races Race {
			get { return (Races)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 0, (byte)value); }
		}

		public virtual Classes Classe {
			get { return (Classes)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 1); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 1, (byte)value); }
		}

		public virtual GenderType Gender {
			get { return (GenderType)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 2); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 2, (byte)value); }
		}

		public virtual PowerType PowerType {
			get { return (PowerType)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 3); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 3, (byte)value); }
		}

		#endregion

		#region UNIT_FIELD_HEALTH
		//UNIT_FIELD_HEALTH : type = Int, size = 1, flag = Public
		public virtual UInt32 Health {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_HEALTH); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_HEALTH, value); }
		}
		#endregion

		#region UNIT_FIELD_POWER
		//UNIT_FIELD_POWER1 : type = Int, size = 1, flag = Public * 7
		public virtual UInt32 Power {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POWER1 + (int)PowerType); }
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
				SetUInt32(UpdateFields.UNIT_FIELD_POWER1 + (int)PowerType, value);
				ObjectManager.SendNearExceptMe(this, GetPowerUpdatePkt(value));
			}
		}
		#endregion

		#region UNIT_FIELD_MAXHEALTH
		//UNIT_FIELD_MAXHEALTH : type = Int, size = 1, flag = Public
		public virtual UInt32 MaxHealth {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_MAXHEALTH); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_MAXHEALTH, value); }
		}
		#endregion

		#region UNIT_FIELD_MAXPOWER
		//UNIT_FIELD_MAXPOWER1 : type = Int, size = 1, flag = Public * 7
		public virtual UInt32 MaxPower {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER1 + (int)PowerType); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_MAXPOWER1 + (int)PowerType, value); }
		}
		#endregion

		#region UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER
		//UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER : type = Single, size = 7, flag = Private, Owner
		public virtual Single PowerRegenFlatModifier {
			get { return GetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER + (int)PowerType); }
			set { SetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER + (int)PowerType, value); }
		}
		#endregion

		#region  UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER
		//UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER : type = Single, size = 7, flag = Private, Owner
		public virtual Single PowerRegenInterruptedFlatModifier {
			get { return GetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER + (int)PowerType); }
			set { SetSingle(UpdateFields.UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER + (int)PowerType, value); }
		}
		#endregion

		#region UNIT_FIELD_LEVEL
		//UNIT_FIELD_LEVEL : type = Int, size = 1, flag = Public
		public virtual UInt32 Level {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_LEVEL); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_LEVEL, value); }
		}
		#endregion

		#region UNIT_FIELD_FACTIONTEMPLATE
		//UNIT_FIELD_FACTIONTEMPLATE : type = Int, size = 1, flag = Public
		public virtual UInt32 FactionTemplate {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_FACTIONTEMPLATE); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_FACTIONTEMPLATE, value); }
		}
		#endregion

		//UNIT_VIRTUAL_ITEM_SLOT_ID : type = Int, size = 3, flag = Public

		#region UNIT_FIELD_FLAGS
		//UNIT_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_FLAGS); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_FLAGS, value); }
		}
		#endregion

		#region UNIT_FIELD_FLAGS_2
		//UNIT_FIELD_FLAGS_2 : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags2 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_FLAGS_2); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_FLAGS_2, value); }
		}
		#endregion

		#region UNIT_FIELD_AURASTATE
		//UNIT_FIELD_AURASTATE : type = Int, size = 1, flag = Public
		public virtual UInt32 AuraState {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_AURASTATE); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_AURASTATE, value); }
		}
		#endregion

		//UNIT_FIELD_BASEATTACKTIME : type = Int, size = 2, flag = Public

		#region UNIT_FIELD_RANGEDATTACKTIME
		//UNIT_FIELD_RANGEDATTACKTIME : type = Int, size = 1, flag = Private
		public virtual UInt32 RangedAttackTime {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_RANGEDATTACKTIME); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_RANGEDATTACKTIME, value); }
		}
		#endregion

		#region UNIT_FIELD_BOUNDINGRADIUS
		//UNIT_FIELD_BOUNDINGRADIUS : type = Single, size = 1, flag = Public
		public virtual Single BoundingRadius {
			get { return GetSingle(UpdateFields.UNIT_FIELD_BOUNDINGRADIUS); }
			set { SetSingle(UpdateFields.UNIT_FIELD_BOUNDINGRADIUS, value); }
		}
		#endregion

		#region UNIT_FIELD_COMBATREACH
		//UNIT_FIELD_COMBATREACH : type = Single, size = 1, flag = Public
		public virtual Single CombatReach {
			get { return GetSingle(UpdateFields.UNIT_FIELD_COMBATREACH); }
			set { SetSingle(UpdateFields.UNIT_FIELD_COMBATREACH, value); }
		}
		#endregion

		#region UNIT_FIELD_DISPLAYID
		//UNIT_FIELD_DISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_DISPLAYID); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_DISPLAYID, value); }
		}
		#endregion

		#region UNIT_FIELD_NATIVEDISPLAYID
		//UNIT_FIELD_NATIVEDISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 NativeDisplayId {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NATIVEDISPLAYID); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NATIVEDISPLAYID, value); }
		}
		#endregion

		#region UNIT_FIELD_MOUNTDISPLAYID
		//UNIT_FIELD_MOUNTDISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 MountDisplayId {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_MOUNTDISPLAYID); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_MOUNTDISPLAYID, value); }
		}
		#endregion

		#region UNIT_FIELD_MINDAMAGE
		//UNIT_FIELD_MINDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MinDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MINDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MINDAMAGE, value); }
		}
		#endregion

		#region UNIT_FIELD_MAXDAMAGE
		//UNIT_FIELD_MAXDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MaxDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MAXDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MAXDAMAGE, value); }
		}
		#endregion

		#region UNIT_FIELD_MINOFFHANDDAMAGE
		//UNIT_FIELD_MINOFFHANDDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MinOffHandDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MINOFFHANDDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MINOFFHANDDAMAGE, value); }
		}
		#endregion

		#region UNIT_FIELD_MAXOFFHANDDAMAGE
		//UNIT_FIELD_MAXOFFHANDDAMAGE : type = Single, size = 1, flag = Private, Owner, PartyLeader
		public virtual Single MaxOffHandDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MAXOFFHANDDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MAXOFFHANDDAMAGE, value); }
		}
		#endregion

		#region UNIT_FIELD_BYTES_1
		//UNIT_FIELD_BYTES_1 : type = Bytes, size = 1, flag = Public

		public virtual StandStates StandState {
			get { return (StandStates)GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 0, (byte)value); }
		}

		public virtual byte PetTalentPoints {
			get { return GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 1); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 1, value); }
		}

		public virtual StateFlag StateFlags {
			get { return (StateFlag)GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 2); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 2, (byte)value); }
		}

		public virtual byte UnitBytes1_3 {
			get { return GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 3); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 3, value); }
		}

		#endregion

		#region UNIT_FIELD_PETNUMBER
		//UNIT_FIELD_PETNUMBER : type = Int, size = 1, flag = Public
		public virtual UInt32 PetNumber {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_PETNUMBER); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_PETNUMBER, value); }
		}
		#endregion

		#region UNIT_FIELD_PET_NAME_TIMESTAMP
		//UNIT_FIELD_PET_NAME_TIMESTAMP : type = Int, size = 1, flag = Public
		public virtual UInt32 PetNameTimestamp {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_PET_NAME_TIMESTAMP); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_PET_NAME_TIMESTAMP, value); }
		}
		#endregion

		#region UNIT_FIELD_PETEXPERIENCE
		//UNIT_FIELD_PETEXPERIENCE : type = Int, size = 1, flag = Owner
		public virtual UInt32 PetXp {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_PETEXPERIENCE); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_PETEXPERIENCE, value); }
		}
		#endregion

		#region UNIT_FIELD_PETNEXTLEVELEXP
		//UNIT_FIELD_PETNEXTLEVELEXP : type = Int, size = 1, flag = Owner
		public virtual UInt32 PetNextLevelXp {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_PETNEXTLEVELEXP); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_PETNEXTLEVELEXP, value); }
		}
		#endregion

		#region UNIT_DYNAMIC_FLAGS
		//UNIT_DYNAMIC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual UInt32 DynamicFlags {
			get { return GetUInt32(UpdateFields.UNIT_DYNAMIC_FLAGS); }
			set { SetUInt32(UpdateFields.UNIT_DYNAMIC_FLAGS, value); }
		}
		#endregion

		#region UNIT_CHANNEL_SPELL
		//UNIT_CHANNEL_SPELL : type = Int, size = 1, flag = Public
		public virtual UInt32 ChannelSpell {
			get { return GetUInt32(UpdateFields.UNIT_CHANNEL_SPELL); }
			set { SetUInt32(UpdateFields.UNIT_CHANNEL_SPELL, value); }
		}
		#endregion

		#region UNIT_MOD_CAST_SPEED
		//UNIT_MOD_CAST_SPEED : type = Single, size = 1, flag = Public
		public virtual Single ModCastSpeed {
			get { return GetSingle(UpdateFields.UNIT_MOD_CAST_SPEED); }
			set { SetSingle(UpdateFields.UNIT_MOD_CAST_SPEED, value); }
		}
		#endregion

		#region UNIT_CREATED_BY_SPELL
		//UNIT_CREATED_BY_SPELL : type = Int, size = 1, flag = Public
		public virtual UInt32 CreatedBySpell {
			get { return GetUInt32(UpdateFields.UNIT_CREATED_BY_SPELL); }
			set { SetUInt32(UpdateFields.UNIT_CREATED_BY_SPELL, value); }
		}
		#endregion

		#region UNIT_NPC_FLAGS
		//UNIT_NPC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual UInt32 NpcFlags {
			get { return GetUInt32(UpdateFields.UNIT_NPC_FLAGS); }
			set { SetUInt32(UpdateFields.UNIT_NPC_FLAGS, value); }
		}
		#endregion

		#region UNIT_NPC_EMOTESTATE
		//UNIT_NPC_EMOTESTATE : type = Int, size = 1, flag = Public
		public virtual UInt32 NpcEmoteState {
			get { return GetUInt32(UpdateFields.UNIT_NPC_EMOTESTATE); }
			set { SetUInt32(UpdateFields.UNIT_NPC_EMOTESTATE, value); }
		}
		#endregion

		#region UNIT_FIELD_STAT0
		//UNIT_FIELD_STAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat0 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_STAT0); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_STAT0, value); }
		}
		#endregion

		#region UNIT_FIELD_STAT1
		//UNIT_FIELD_STAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat1 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_STAT1); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_STAT1, value); }
		}
		#endregion

		#region UNIT_FIELD_STAT2
		//UNIT_FIELD_STAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat2 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_STAT2); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_STAT2, value); }
		}
		#endregion

		#region UNIT_FIELD_STAT3
		//UNIT_FIELD_STAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat3 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_STAT3); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_STAT3, value); }
		}
		#endregion

		#region UNIT_FIELD_STAT4
		//UNIT_FIELD_STAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 Stat4 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_STAT4); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_STAT4, value); }
		}
		#endregion

		#region UNIT_FIELD_POSSTAT0
		//UNIT_FIELD_POSSTAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat0 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POSSTAT0); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT0, value); }
		}
		#endregion

		#region UNIT_FIELD_POSSTAT1
		//UNIT_FIELD_POSSTAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat1 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POSSTAT1); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT1, value); }
		}
		#endregion

		#region UNIT_FIELD_POSSTAT2
		//UNIT_FIELD_POSSTAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat2 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POSSTAT2); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT2, value); }
		}
		#endregion

		#region UNIT_FIELD_POSSTAT3
		//UNIT_FIELD_POSSTAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat3 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POSSTAT3); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT3, value); }
		}
		#endregion

		#region UNIT_FIELD_POSSTAT4
		//UNIT_FIELD_POSSTAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 PosStat4 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_POSSTAT4); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_POSSTAT4, value); }
		}
		#endregion

		#region UNIT_FIELD_NEGSTAT0
		//UNIT_FIELD_NEGSTAT0 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat0 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT0); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT0, value); }
		}
		#endregion

		#region UNIT_FIELD_NEGSTAT1
		//UNIT_FIELD_NEGSTAT1 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat1 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT1); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT1, value); }
		}
		#endregion

		#region UNIT_FIELD_NEGSTAT2
		//UNIT_FIELD_NEGSTAT2 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat2 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT2); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT2, value); }
		}
		#endregion

		#region UNIT_FIELD_NEGSTAT3
		//UNIT_FIELD_NEGSTAT3 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat3 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT3); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT3, value); }
		}
		#endregion

		#region UNIT_FIELD_NEGSTAT4
		//UNIT_FIELD_NEGSTAT4 : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 NegStat4 {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT4); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_NEGSTAT4, value); }
		}
		#endregion

		//UNIT_FIELD_RESISTANCES : type = Int, size = 7, flag = Private, Owner, PartyLeader

		//UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE : type = Int, size = 7, flag = Private, Owner

		//UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE : type = Int, size = 7, flag = Private, Owner

		#region UNIT_FIELD_BASE_MANA
		//UNIT_FIELD_BASE_MANA : type = Int, size = 1, flag = Public
		public virtual UInt32 BaseMana {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_BASE_MANA); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_BASE_MANA, value); }
		}
		#endregion

		#region UNIT_FIELD_BASE_HEALTH
		//UNIT_FIELD_BASE_HEALTH : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 BaseHealth {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_BASE_HEALTH); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_BASE_HEALTH, value); }
		}
		#endregion

		#region UNIT_FIELD_BYTES_2
		//UNIT_FIELD_BYTES_2 : type = Bytes, size = 1, flag = Public
		public SheathType Sheath {
			get { return (SheathType)GetByte(UpdateFields.UNIT_FIELD_BYTES_2, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_2, 0, (byte)value); }
		}

		public PvPState PvpState {
			get { return (PvPState)GetByte(UpdateFields.UNIT_FIELD_BYTES_2, 1); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_2, 1, (byte)value); }
		}

		public PetState PetState {
			get { return (PetState)GetByte(UpdateFields.UNIT_FIELD_BYTES_2, 2); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_2, 2, (byte)value); }
		}

		public ShapeShiftForm ShapeShiftForm {
			get { return (ShapeShiftForm)GetByte(UpdateFields.UNIT_FIELD_BYTES_2, 3); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_2, 3, (byte)value); }
		}

		#endregion

		#region UNIT_FIELD_ATTACK_POWER
		//UNIT_FIELD_ATTACK_POWER : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 AttackPower {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER, value); }
		}
		#endregion

		#region UNIT_FIELD_ATTACK_POWER_MODS
		//UNIT_FIELD_ATTACK_POWER_MODS : type = Shorts, size = 1, flag = Private, Owner
		public virtual UInt32 AttackPowerMods {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER_MODS); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_ATTACK_POWER_MODS, value); }
		}
		#endregion

		#region UNIT_FIELD_ATTACK_POWER_MULTIPLIER
		//UNIT_FIELD_ATTACK_POWER_MULTIPLIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single AttackPowerMultiplier {
			get { return GetSingle(UpdateFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER); }
			set { SetSingle(UpdateFields.UNIT_FIELD_ATTACK_POWER_MULTIPLIER, value); }
		}
		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER
		//UNIT_FIELD_RANGED_ATTACK_POWER : type = Int, size = 1, flag = Private, Owner
		public virtual UInt32 RangedAttackPower {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER, value); }
		}
		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER_MODS
		//UNIT_FIELD_RANGED_ATTACK_POWER_MODS : type = Shorts, size = 1, flag = Private, Owner
		public virtual UInt32 RangedAttackPowerMods {
			get { return GetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS); }
			set { SetUInt32(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MODS, value); }
		}
		#endregion

		#region UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER
		//UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single RangedAttackPowerMultiplier {
			get { return GetSingle(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER); }
			set { SetSingle(UpdateFields.UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER, value); }
		}
		#endregion

		#region UNIT_FIELD_MINRANGEDDAMAGE
		//UNIT_FIELD_MINRANGEDDAMAGE : type = Single, size = 1, flag = Private, Owner
		public virtual Single MinRangedDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MINRANGEDDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MINRANGEDDAMAGE, value); }
		}
		#endregion

		#region UNIT_FIELD_MAXRANGEDDAMAGE
		//UNIT_FIELD_MAXRANGEDDAMAGE : type = Single, size = 1, flag = Private, Owner
		public virtual Single MaxRangedDamage {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MAXRANGEDDAMAGE); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MAXRANGEDDAMAGE, value); }
		}
		#endregion

		//UNIT_FIELD_POWER_COST_MODIFIER : type = Int, size = 7, flag = Private, Owner

		//UNIT_FIELD_POWER_COST_MULTIPLIER : type = Single, size = 7, flag = Private, Owner

		#region UNIT_FIELD_MAXHEALTHMODIFIER
		//UNIT_FIELD_MAXHEALTHMODIFIER : type = Single, size = 1, flag = Private, Owner
		public virtual Single MaxHealthModifier {
			get { return GetSingle(UpdateFields.UNIT_FIELD_MAXHEALTHMODIFIER); }
			set { SetSingle(UpdateFields.UNIT_FIELD_MAXHEALTHMODIFIER, value); }
		}
		#endregion

		#region UNIT_FIELD_HOVERHEIGHT
		//UNIT_FIELD_HOVERHEIGHT : type = Single, size = 1, flag = Public
		public virtual Single HoverHeight {
			get { return GetSingle(UpdateFields.UNIT_FIELD_HOVERHEIGHT); }
			set { SetSingle(UpdateFields.UNIT_FIELD_HOVERHEIGHT, value); }
		}
		#endregion

		//UNIT_FIELD_PADDING : type = Int, size = 1, flag = None

	}
}
