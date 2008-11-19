using System;

namespace Hazzik.Objects {
	public class Unit : Mobile {
		public Unit()
			: this((int)UpdateFields.UNIT_END, 0x09) {
		}

		protected Unit(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Unit; }
		}

		public override byte UpdateFlag {
			get { return base.UpdateFlag; }
		}

		#region UpdateFields

		//UNIT_FIELD_CHARM = OBJECT_END + 0, // 2 4 1
		public long CharmGuid { get; set; }
		//UNIT_FIELD_SUMMON = OBJECT_END + 2, // 2 4 1
		public long SummonGuid { get; set; }
		//UNIT_FIELD_CRITTER = OBJECT_END + 4, // 2 4 2
		public long CritterGuid { get; set; }
		//UNIT_FIELD_CHARMEDBY = OBJECT_END + 6, // 2 4 1
		public long CharmedByGuid { get; set; }
		//UNIT_FIELD_SUMMONEDBY = OBJECT_END + 8, // 2 4 1
		public long SummonedByGuid { get; set; }
		//UNIT_FIELD_CREATEDBY = OBJECT_END + 10, // 2 4 1
		public long CreatedByGuid { get; set; }
		//UNIT_FIELD_TARGET = OBJECT_END + 12, // 2 4 1
		public long TargetGuid { get; set; }
		//UNIT_FIELD_CHANNEL_OBJECT = OBJECT_END + 14, // 2 4 1
		public long ChannelObjectGuid { get; set; }
		//UNIT_FIELD_HEALTH = OBJECT_END + 16, // 1 1 256
		public int Health { get; set; }
		//UNIT_FIELD_POWER1 = OBJECT_END + 17, // 1 1 1
		public int Power1 { get; set; }
		//UNIT_FIELD_POWER2 = OBJECT_END + 18, // 1 1 1
		public int Power2 { get; set; }
		//UNIT_FIELD_POWER3 = OBJECT_END + 19, // 1 1 1
		public int Power3 { get; set; }
		//UNIT_FIELD_POWER4 = OBJECT_END + 20, // 1 1 1
		public int Power4 { get; set; }
		//UNIT_FIELD_POWER5 = OBJECT_END + 21, // 1 1 1
		public int Power5 { get; set; }
		//UNIT_FIELD_POWER6 = OBJECT_END + 22, // 1 1 1
		public int Power6 { get; set; }
		//UNIT_FIELD_POWER7 = OBJECT_END + 23, // 1 1 1
		public int Power7 { get; set; }
		//UNIT_FIELD_MAXHEALTH = OBJECT_END + 24, // 1 1 256
		public int MaxHealth { get; set; }
		//UNIT_FIELD_MAXPOWER1 = OBJECT_END + 25, // 1 1 1
		public int MaxPower1 { get; set; }
		//UNIT_FIELD_MAXPOWER2 = OBJECT_END + 26, // 1 1 1
		public int MaxPower2 { get; set; }
		//UNIT_FIELD_MAXPOWER3 = OBJECT_END + 27, // 1 1 1
		public int MaxPower3 { get; set; }
		//UNIT_FIELD_MAXPOWER4 = OBJECT_END + 28, // 1 1 1
		public int MaxPower4 { get; set; }
		//UNIT_FIELD_MAXPOWER5 = OBJECT_END + 29, // 1 1 1
		public int MaxPower5 { get; set; }
		//UNIT_FIELD_MAXPOWER6 = OBJECT_END + 30, // 1 1 1
		public int MaxPower6 { get; set; }
		//UNIT_FIELD_MAXPOWER7 = OBJECT_END + 31, // 1 1 1
		public int MaxPower7 { get; set; }
		//UNIT_FIELD_LEVEL = OBJECT_END + 32, // 1 1 1
		public uint Level { get; set; }
		//UNIT_FIELD_FACTIONTEMPLATE = OBJECT_END + 33, // 1 1 1
		public int FactionTemplate { get; set; }
		//UNIT_FIELD_BYTES_0 = OBJECT_END + 34, // 1 5 1
		public int Bytes0 { get; set; }
		//UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = OBJECT_END + 35, // 3 1 1
		//UNIT_VIRTUAL_ITEM_INFO = OBJECT_END + 38, // 6 5 1
		//UNIT_FIELD_FLAGS = OBJECT_END + 44, // 1 1 1
		//UNIT_FIELD_FLAGS_2 = OBJECT_END + 45, // 1 1 1
		//UNIT_FIELD_AURA = OBJECT_END + 46, // 56 1 1
		//UNIT_FIELD_AURAFLAGS = OBJECT_END + 102, // 14 5 1
		//UNIT_FIELD_AURALEVELS = OBJECT_END + 116, // 14 5 1
		//UNIT_FIELD_AURAAPPLICATIONS = OBJECT_END + 130, // 14 5 1
		//UNIT_FIELD_AURASTATE = OBJECT_END + 144, // 1 1 1
		//UNIT_FIELD_BASEATTACKTIME = OBJECT_END + 145, // 2 1 1
		//UNIT_FIELD_RANGEDATTACKTIME = OBJECT_END + 147, // 1 1 2
		//UNIT_FIELD_BOUNDINGRADIUS = OBJECT_END + 148, // 1 3 1
		//UNIT_FIELD_COMBATREACH = OBJECT_END + 149, // 1 3 1
		//UNIT_FIELD_DISPLAYID = OBJECT_END + 150, // 1 1 1
		//UNIT_FIELD_NATIVEDISPLAYID = OBJECT_END + 151, // 1 1 1
		//UNIT_FIELD_MOUNTDISPLAYID = OBJECT_END + 152, // 1 1 1
		//UNIT_FIELD_MINDAMAGE = OBJECT_END + 153, // 1 3 38
		//UNIT_FIELD_MAXDAMAGE = OBJECT_END + 154, // 1 3 38
		//UNIT_FIELD_MINOFFHANDDAMAGE = OBJECT_END + 155, // 1 3 38
		//UNIT_FIELD_MAXOFFHANDDAMAGE = OBJECT_END + 156, // 1 3 38
		//UNIT_FIELD_BYTES_1 = OBJECT_END + 157, // 1 5 1
		//UNIT_FIELD_PETNUMBER = OBJECT_END + 158, // 1 1 1
		//UNIT_FIELD_PET_NAME_TIMESTAMP = OBJECT_END + 159, // 1 1 1
		//UNIT_FIELD_PETEXPERIENCE = OBJECT_END + 160, // 1 1 4
		//UNIT_FIELD_PETNEXTLEVELEXP = OBJECT_END + 161, // 1 1 4
		//UNIT_DYNAMIC_FLAGS = OBJECT_END + 162, // 1 1 256
		//UNIT_CHANNEL_SPELL = OBJECT_END + 163, // 1 1 1
		//UNIT_MOD_CAST_SPEED = OBJECT_END + 164, // 1 3 1
		//UNIT_CREATED_BY_SPELL = OBJECT_END + 165, // 1 1 1
		//UNIT_NPC_FLAGS = OBJECT_END + 166, // 1 1 256
		//UNIT_NPC_EMOTESTATE = OBJECT_END + 167, // 1 1 1
		//UNIT_FIELD_STAT0 = OBJECT_END + 168, // 1 1 6
		//UNIT_FIELD_STAT1 = OBJECT_END + 169, // 1 1 6
		//UNIT_FIELD_STAT2 = OBJECT_END + 170, // 1 1 6
		//UNIT_FIELD_STAT3 = OBJECT_END + 171, // 1 1 6
		//UNIT_FIELD_STAT4 = OBJECT_END + 172, // 1 1 6
		//UNIT_FIELD_POSSTAT0 = OBJECT_END + 173, // 1 1 6
		//UNIT_FIELD_POSSTAT1 = OBJECT_END + 174, // 1 1 6
		//UNIT_FIELD_POSSTAT2 = OBJECT_END + 175, // 1 1 6
		//UNIT_FIELD_POSSTAT3 = OBJECT_END + 176, // 1 1 6
		//UNIT_FIELD_POSSTAT4 = OBJECT_END + 177, // 1 1 6
		//UNIT_FIELD_NEGSTAT0 = OBJECT_END + 178, // 1 1 6
		//UNIT_FIELD_NEGSTAT1 = OBJECT_END + 179, // 1 1 6
		//UNIT_FIELD_NEGSTAT2 = OBJECT_END + 180, // 1 1 6
		//UNIT_FIELD_NEGSTAT3 = OBJECT_END + 181, // 1 1 6
		//UNIT_FIELD_NEGSTAT4 = OBJECT_END + 182, // 1 1 6
		//UNIT_FIELD_RESISTANCES = OBJECT_END + 183, // 7 1 38
		//UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE = OBJECT_END + 190, // 7 1 6
		//UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE = OBJECT_END + 197, // 7 1 6
		//UNIT_FIELD_BASE_MANA = OBJECT_END + 204, // 1 1 1
		//UNIT_FIELD_BASE_HEALTH = OBJECT_END + 205, // 1 1 6
		//UNIT_FIELD_BYTES_2 = OBJECT_END + 206, // 1 5 1
		//UNIT_FIELD_ATTACK_POWER = OBJECT_END + 207, // 1 1 6
		//UNIT_FIELD_ATTACK_POWER_MODS = OBJECT_END + 208, // 1 2 6
		//UNIT_FIELD_ATTACK_POWER_MULTIPLIER = OBJECT_END + 209, // 1 3 6
		//UNIT_FIELD_RANGED_ATTACK_POWER = OBJECT_END + 210, // 1 1 6
		//UNIT_FIELD_RANGED_ATTACK_POWER_MODS = OBJECT_END + 211, // 1 2 6
		//UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = OBJECT_END + 212, // 1 3 6
		//UNIT_FIELD_MINRANGEDDAMAGE = OBJECT_END + 213, // 1 3 6
		//UNIT_FIELD_MAXRANGEDDAMAGE = OBJECT_END + 214, // 1 3 6
		//UNIT_FIELD_POWER_COST_MODIFIER = OBJECT_END + 215, // 7 1 6
		//UNIT_FIELD_POWER_COST_MULTIPLIER = OBJECT_END + 222, // 7 3 6
		//UNIT_FIELD_MAXHEALTHMODIFIER = OBJECT_END + 229, // 1 3 6
		//UNIT_FIELD_HOVERHEIGHT = OBJECT_END + 230, // 1 3 1
		//UNIT_FIELD_PADDING = OBJECT_END + 231, // 1 1 0
		//UNIT_END = OBJECT_END + 232,

		#endregion
	}
}