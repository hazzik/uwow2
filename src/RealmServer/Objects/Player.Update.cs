using System;

namespace Hazzik.Objects {
	public partial class Player {
		#region PLAYER_DUEL_ARBITER
		//PLAYER_DUEL_ARBITER : type = Long, size = 2, flag = Public
		public virtual UInt64 DuelArbiterGuid {
			get { return GetUInt64(UpdateFields.PLAYER_DUEL_ARBITER); }
			set { SetUInt64(UpdateFields.PLAYER_DUEL_ARBITER, value); }
		}
		#endregion

		#region PLAYER_FLAGS
		//PLAYER_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags {
			get { return GetUInt32(UpdateFields.PLAYER_FLAGS); }
			set { SetUInt32(UpdateFields.PLAYER_FLAGS, value); }
		}
		#endregion

		#region PLAYER_GUILDID
		//PLAYER_GUILDID : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildId {
			get { return GetUInt32(UpdateFields.PLAYER_GUILDID); }
			set { SetUInt32(UpdateFields.PLAYER_GUILDID, value); }
		}
		#endregion

		#region PLAYER_GUILDRANK
		//PLAYER_GUILDRANK : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildRank {
			get { return GetUInt32(UpdateFields.PLAYER_GUILDRANK); }
			set { SetUInt32(UpdateFields.PLAYER_GUILDRANK, value); }
		}
		#endregion

		#region PLAYER_BYTES
		//PLAYER_BYTES : type = Bytes, size = 1, flag = Public
		public byte Skin {
			get { return GetByte(UpdateFields.PLAYER_BYTES, 0); }
			set { SetByte(UpdateFields.PLAYER_BYTES, 0, value); }
		}

		public byte Face {
			get { return GetByte(UpdateFields.PLAYER_BYTES, 1); }
			set { SetByte(UpdateFields.PLAYER_BYTES, 1, value); }
		}

		public byte HairStyle {
			get { return GetByte(UpdateFields.PLAYER_BYTES, 2); }
			set { SetByte(UpdateFields.PLAYER_BYTES, 2, value); }
		}

		public byte HairColor {
			get { return GetByte(UpdateFields.PLAYER_BYTES, 3); }
			set { SetByte(UpdateFields.PLAYER_BYTES, 3, value); }
		}

		#endregion

		#region PLAYER_BYTES_2
		//PLAYER_BYTES_2 : type = Bytes, size = 1, flag = Public
		public byte FacialHair {
			get { return GetByte(UpdateFields.PLAYER_BYTES_2, 0); }
			set { SetByte(UpdateFields.PLAYER_BYTES_2, 0, value); }
		}

		public byte PlayerBytes2_2 {
			get { return GetByte(UpdateFields.PLAYER_BYTES_2, 1); }
			set { SetByte(UpdateFields.PLAYER_BYTES_2, 1, value); }
		}

		public byte BankBagSlots {
			get { return GetByte(UpdateFields.PLAYER_BYTES_2, 2); }
			internal set { SetByte(UpdateFields.PLAYER_BYTES_2, 2, value); }
		}

		public RestState RestState {
			get { return (RestState)GetByte(UpdateFields.PLAYER_BYTES_2, 3); }
			set { SetByte(UpdateFields.PLAYER_BYTES_2, 3, (byte)value); }
		}

		#endregion

		#region PLAYER_BYTES_3
		//PLAYER_BYTES_3 : type = Bytes, size = 1, flag = Public
		public override GenderType Gender {
			get { return (GenderType)GetByte(UpdateFields.PLAYER_BYTES_3, 0); }
			set {
				SetByte(UpdateFields.PLAYER_BYTES_3, 0, (byte)value);
				base.Gender = value;
			}
		}

		public virtual byte DrunkState {
			get { return GetByte(UpdateFields.PLAYER_BYTES_3, 1); }
			set {
				if(value > 100)
					value = 100;
				SetByte(UpdateFields.PLAYER_BYTES_3, 1, value);
			}
		}

		public virtual byte PlayerBytes3_3 {
			get { return GetByte(UpdateFields.PLAYER_BYTES_3, 2); }
			set { SetByte(UpdateFields.PLAYER_BYTES_3, 2, value); }
		}

		public virtual byte PvPRank {
			get { return GetByte(UpdateFields.PLAYER_BYTES_3, 3); }
			set { SetByte(UpdateFields.PLAYER_BYTES_3, 3, value); }
		}

		#endregion

		#region PLAYER_DUEL_TEAM
		//PLAYER_DUEL_TEAM : type = Int, size = 1, flag = Public
		public virtual UInt32 DuelTeam {
			get { return GetUInt32(UpdateFields.PLAYER_DUEL_TEAM); }
			set { SetUInt32(UpdateFields.PLAYER_DUEL_TEAM, value); }
		}
		#endregion

		#region PLAYER_GUILD_TIMESTAMP
		//PLAYER_GUILD_TIMESTAMP : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildTimestamp {
			get { return GetUInt32(UpdateFields.PLAYER_GUILD_TIMESTAMP); }
			set { SetUInt32(UpdateFields.PLAYER_GUILD_TIMESTAMP, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_1_1 * 25
		//PLAYER_QUEST_LOG_1_1 : type = Int, size = 1, flag = PartyMember
		//PLAYER_QUEST_LOG_1_2 : type = Int, size = 1, flag = Private
		//PLAYER_QUEST_LOG_1_3 : type = Bytes, size = 1, flag = Private
		//PLAYER_QUEST_LOG_1_4 : type = Int, size = 1, flag = Private
		#endregion

		#region PLAYER_VISIBLE_ITEM_1	* 19
		//PLAYER_VISIBLE_ITEM_1_CREATOR : type = Long, size = 2, flag = Public
		//PLAYER_VISIBLE_ITEM_1_0 : type = Int, size = 13, flag = Public
		//PLAYER_VISIBLE_ITEM_1_PROPERTIES : type = Shorts, size = 1, flag = Public
		//PLAYER_VISIBLE_ITEM_1_SEED : type = Int, size = 1, flag = Public
		//PLAYER_VISIBLE_ITEM_1_PAD : type = Int, size = 1, flag = Public
		#endregion

		#region PLAYER_CHOSEN_TITLE
		//PLAYER_CHOSEN_TITLE : type = Int, size = 1, flag = Public
		public virtual UInt32 ChosenTitle {
			get { return GetUInt32(UpdateFields.PLAYER_CHOSEN_TITLE); }
			set { SetUInt32(UpdateFields.PLAYER_CHOSEN_TITLE, value); }
		}
		#endregion

		//PLAYER_FIELD_PAD_0 : type = Int, size = 1, flag = None

		//PLAYER_FIELD_INV_SLOT_HEAD : type = Long, size = 46, flag = Private

		//PLAYER_FIELD_PACK_SLOT_1 : type = Long, size = 32, flag = Private

		//PLAYER_FIELD_BANK_SLOT_1 : type = Long, size = 56, flag = Private

		//PLAYER_FIELD_BANKBAG_SLOT_1 : type = Long, size = 14, flag = Private

		//PLAYER_FIELD_VENDORBUYBACK_SLOT_1 : type = Long, size = 24, flag = Private

		//PLAYER_FIELD_KEYRING_SLOT_1 : type = Long, size = 64, flag = Private

		//PLAYER_FIELD_VANITYPET_SLOT_1 : type = Long, size = 36, flag = Private

		//PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 : type = Long, size = 64, flag = Private

		//PLAYER_FIELD_QUESTBAG_SLOT_1 : type = Long, size = 64, flag = Private

		#region PLAYER_FARSIGHT
		//PLAYER_FARSIGHT : type = Long, size = 2, flag = Private
		public virtual UInt64 FarSightGuid {
			get { return GetUInt64(UpdateFields.PLAYER_FARSIGHT); }
			set { SetUInt64(UpdateFields.PLAYER_FARSIGHT, value); }
		}
		#endregion

		#region PLAYER__FIELD_KNOWN_TITLES
		//PLAYER__FIELD_KNOWN_TITLES : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownTitlesGuid {
			get { return GetUInt64(UpdateFields.PLAYER__FIELD_KNOWN_TITLES); }
			set { SetUInt64(UpdateFields.PLAYER__FIELD_KNOWN_TITLES, value); }
		}
		#endregion

		#region PLAYER__FIELD_KNOWN_TITLES1
		//PLAYER__FIELD_KNOWN_TITLES1 : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownTitles1Guid {
			get { return GetUInt64(UpdateFields.PLAYER__FIELD_KNOWN_TITLES1); }
			set { SetUInt64(UpdateFields.PLAYER__FIELD_KNOWN_TITLES1, value); }
		}
		#endregion

		#region PLAYER_FIELD_KNOWN_CURRENCIES
		//PLAYER_FIELD_KNOWN_CURRENCIES : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownCurrenciesGuid {
			get { return GetUInt64(UpdateFields.PLAYER_FIELD_KNOWN_CURRENCIES); }
			set { SetUInt64(UpdateFields.PLAYER_FIELD_KNOWN_CURRENCIES, value); }
		}
		#endregion

		#region PLAYER_XP
		//PLAYER_XP : type = Int, size = 1, flag = Private
		public virtual UInt32 Xp {
			get { return GetUInt32(UpdateFields.PLAYER_XP); }
			set { SetUInt32(UpdateFields.PLAYER_XP, value); }
		}
		#endregion

		#region PLAYER_NEXT_LEVEL_XP
		//PLAYER_NEXT_LEVEL_XP : type = Int, size = 1, flag = Private
		public virtual UInt32 NextLevelXp {
			get { return GetUInt32(UpdateFields.PLAYER_NEXT_LEVEL_XP); }
			set { SetUInt32(UpdateFields.PLAYER_NEXT_LEVEL_XP, value); }
		}
		#endregion

		//PLAYER_SKILL_INFO_1_1 : type = Shorts, size = 384, flag = Private

		#region PLAYER_CHARACTER_POINTS1
		//PLAYER_CHARACTER_POINTS1 : type = Int, size = 1, flag = Private
		public virtual UInt32 CharacterPoints1 {
			get { return GetUInt32(UpdateFields.PLAYER_CHARACTER_POINTS1); }
			set { SetUInt32(UpdateFields.PLAYER_CHARACTER_POINTS1, value); }
		}
		#endregion

		#region PLAYER_CHARACTER_POINTS2
		//PLAYER_CHARACTER_POINTS2 : type = Int, size = 1, flag = Private
		public virtual UInt32 CharacterPoints2 {
			get { return GetUInt32(UpdateFields.PLAYER_CHARACTER_POINTS2); }
			set { SetUInt32(UpdateFields.PLAYER_CHARACTER_POINTS2, value); }
		}
		#endregion

		#region PLAYER_TRACK_CREATURES
		//PLAYER_TRACK_CREATURES : type = Int, size = 1, flag = Private
		public virtual UInt32 TrackCreatures {
			get { return GetUInt32(UpdateFields.PLAYER_TRACK_CREATURES); }
			set { SetUInt32(UpdateFields.PLAYER_TRACK_CREATURES, value); }
		}
		#endregion

		#region PLAYER_TRACK_RESOURCES
		//PLAYER_TRACK_RESOURCES : type = Int, size = 1, flag = Private
		public virtual UInt32 TrackResources {
			get { return GetUInt32(UpdateFields.PLAYER_TRACK_RESOURCES); }
			set { SetUInt32(UpdateFields.PLAYER_TRACK_RESOURCES, value); }
		}
		#endregion

		#region PLAYER_BLOCK_PERCENTAGE
		//PLAYER_BLOCK_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single BlockPercentage {
			get { return GetSingle(UpdateFields.PLAYER_BLOCK_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_BLOCK_PERCENTAGE, value); }
		}
		#endregion

		#region PLAYER_DODGE_PERCENTAGE
		//PLAYER_DODGE_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single DodgePercentage {
			get { return GetSingle(UpdateFields.PLAYER_DODGE_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_DODGE_PERCENTAGE, value); }
		}
		#endregion

		#region PLAYER_PARRY_PERCENTAGE
		//PLAYER_PARRY_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single ParryPercentage {
			get { return GetSingle(UpdateFields.PLAYER_PARRY_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_PARRY_PERCENTAGE, value); }
		}
		#endregion

		#region PLAYER_EXPERTISE
		//PLAYER_EXPERTISE : type = Int, size = 1, flag = Private
		public virtual UInt32 Expertise {
			get { return GetUInt32(UpdateFields.PLAYER_EXPERTISE); }
			set { SetUInt32(UpdateFields.PLAYER_EXPERTISE, value); }
		}
		#endregion

		#region PLAYER_OFFHAND_EXPERTISE
		//PLAYER_OFFHAND_EXPERTISE : type = Int, size = 1, flag = Private
		public virtual UInt32 OffHandExpertise {
			get { return GetUInt32(UpdateFields.PLAYER_OFFHAND_EXPERTISE); }
			set { SetUInt32(UpdateFields.PLAYER_OFFHAND_EXPERTISE, value); }
		}
		#endregion

		#region PLAYER_CRIT_PERCENTAGE
		//PLAYER_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single CritPercentage {
			get { return GetSingle(UpdateFields.PLAYER_CRIT_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_CRIT_PERCENTAGE, value); }
		}
		#endregion

		#region PLAYER_RANGED_CRIT_PERCENTAGE
		//PLAYER_RANGED_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single RangedCritPercentage {
			get { return GetSingle(UpdateFields.PLAYER_RANGED_CRIT_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_RANGED_CRIT_PERCENTAGE, value); }
		}
		#endregion

		#region PLAYER_OFFHAND_CRIT_PERCENTAGE
		//PLAYER_OFFHAND_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single OffHandCritPercentage {
			get { return GetSingle(UpdateFields.PLAYER_OFFHAND_CRIT_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_OFFHAND_CRIT_PERCENTAGE, value); }
		}
		#endregion

		//PLAYER_SPELL_CRIT_PERCENTAGE1 : type = Single, size = 7, flag = Private

		#region PLAYER_SHIELD_BLOCK
		//PLAYER_SHIELD_BLOCK : type = Int, size = 1, flag = Private
		public virtual UInt32 ShieldBlock {
			get { return GetUInt32(UpdateFields.PLAYER_SHIELD_BLOCK); }
			set { SetUInt32(UpdateFields.PLAYER_SHIELD_BLOCK, value); }
		}
		#endregion

		#region PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE
		//PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single ShieldBlockCritPercentage {
			get { return GetSingle(UpdateFields.PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE); }
			set { SetSingle(UpdateFields.PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE, value); }
		}
		#endregion

		//PLAYER_EXPLORED_ZONES_1 : type = Bytes, size = 128, flag = Private

		#region PLAYER_REST_STATE_EXPERIENCE
		//PLAYER_REST_STATE_EXPERIENCE : type = Int, size = 1, flag = Private
		public virtual UInt32 RestStateExperience {
			get { return GetUInt32(UpdateFields.PLAYER_REST_STATE_EXPERIENCE); }
			set { SetUInt32(UpdateFields.PLAYER_REST_STATE_EXPERIENCE, value); }
		}
		#endregion

		#region PLAYER_FIELD_COINAGE
		//PLAYER_FIELD_COINAGE : type = Int, size = 1, flag = Private
		public virtual UInt32 Coinage {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_COINAGE); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_COINAGE, value); }
		}
		#endregion

		//PLAYER_FIELD_MOD_DAMAGE_DONE_POS : type = Int, size = 7, flag = Private

		//PLAYER_FIELD_MOD_DAMAGE_DONE_NEG : type = Int, size = 7, flag = Private

		//PLAYER_FIELD_MOD_DAMAGE_DONE_PCT : type = Int, size = 7, flag = Private

		#region PLAYER_FIELD_MOD_HEALING_DONE_POS
		//PLAYER_FIELD_MOD_HEALING_DONE_POS : type = Int, size = 1, flag = Private
		public virtual UInt32 ModHealingDonePos {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_MOD_HEALING_DONE_POS); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_MOD_HEALING_DONE_POS, value); }
		}
		#endregion

		#region PLAYER_FIELD_MOD_TARGET_RESISTANCE
		//PLAYER_FIELD_MOD_TARGET_RESISTANCE : type = Int, size = 1, flag = Private
		public virtual UInt32 ModTargetResistance {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_MOD_TARGET_RESISTANCE); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_MOD_TARGET_RESISTANCE, value); }
		}
		#endregion

		#region PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE
		//PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE : type = Int, size = 1, flag = Private
		public virtual UInt32 ModTargetPhysicalResistance {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE, value); }
		}
		#endregion

		#region PLAYER_FIELD_BYTES
		//PLAYER_FIELD_BYTES : type = Bytes, size = 1, flag = Private
		public virtual UInt32 FieldBytes {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_BYTES); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_BYTES, value); }
		}
		#endregion

		#region PLAYER_AMMO_ID
		//PLAYER_AMMO_ID : type = Int, size = 1, flag = Private
		public virtual UInt32 AmmoId {
			get { return GetUInt32(UpdateFields.PLAYER_AMMO_ID); }
			set { SetUInt32(UpdateFields.PLAYER_AMMO_ID, value); }
		}
		#endregion

		#region PLAYER_SELF_RES_SPELL
		//PLAYER_SELF_RES_SPELL : type = Int, size = 1, flag = Private
		public virtual UInt32 SelfResSpell {
			get { return GetUInt32(UpdateFields.PLAYER_SELF_RES_SPELL); }
			set { SetUInt32(UpdateFields.PLAYER_SELF_RES_SPELL, value); }
		}
		#endregion

		#region PLAYER_FIELD_PVP_MEDALS
		//PLAYER_FIELD_PVP_MEDALS : type = Int, size = 1, flag = Private
		public virtual UInt32 PvpMedals {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_PVP_MEDALS); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_PVP_MEDALS, value); }
		}
		#endregion

		//PLAYER_FIELD_BUYBACK_PRICE_1 : type = Int, size = 12, flag = Private

		//PLAYER_FIELD_BUYBACK_TIMESTAMP_1 : type = Int, size = 12, flag = Private

		#region PLAYER_FIELD_KILLS
		//PLAYER_FIELD_KILLS : type = Shorts, size = 1, flag = Private
		public virtual UInt32 Kills {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_KILLS); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_KILLS, value); }
		}
		#endregion

		#region PLAYER_FIELD_TODAY_CONTRIBUTION
		//PLAYER_FIELD_TODAY_CONTRIBUTION : type = Int, size = 1, flag = Private
		public virtual UInt32 TodayContribution {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_TODAY_CONTRIBUTION); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_TODAY_CONTRIBUTION, value); }
		}
		#endregion

		#region PLAYER_FIELD_YESTERDAY_CONTRIBUTION
		//PLAYER_FIELD_YESTERDAY_CONTRIBUTION : type = Int, size = 1, flag = Private
		public virtual UInt32 YesterdayContribution {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_YESTERDAY_CONTRIBUTION, value); }
		}
		#endregion

		#region PLAYER_FIELD_LIFETIME_HONORBALE_KILLS
		//PLAYER_FIELD_LIFETIME_HONORBALE_KILLS : type = Int, size = 1, flag = Private
		public virtual UInt32 LifetimeHonorbaleKills {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_LIFETIME_HONORBALE_KILLS); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_LIFETIME_HONORBALE_KILLS, value); }
		}
		#endregion

		#region PLAYER_FIELD_BYTES2
		//PLAYER_FIELD_BYTES2 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 FieldBytes2 {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_BYTES2); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_BYTES2, value); }
		}
		#endregion

		#region PLAYER_FIELD_WATCHED_FACTION_INDEX
		//PLAYER_FIELD_WATCHED_FACTION_INDEX : type = Int, size = 1, flag = Private
		public virtual UInt32 WatchedFactionIndex {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_WATCHED_FACTION_INDEX); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, value); }
		}
		#endregion

		//PLAYER_FIELD_COMBAT_RATING_1 : type = Int, size = 25, flag = Private

		//PLAYER_FIELD_ARENA_TEAM_INFO_1_1 : type = Int, size = 18, flag = Private

		#region PLAYER_FIELD_HONOR_CURRENCY
		//PLAYER_FIELD_HONOR_CURRENCY : type = Int, size = 1, flag = Private
		public virtual UInt32 HonorCurrency {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_HONOR_CURRENCY); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_HONOR_CURRENCY, value); }
		}
		#endregion

		#region PLAYER_FIELD_ARENA_CURRENCY
		//PLAYER_FIELD_ARENA_CURRENCY : type = Int, size = 1, flag = Private
		public virtual UInt32 ArenaCurrency {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_ARENA_CURRENCY); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_ARENA_CURRENCY, value); }
		}
		#endregion

		#region PLAYER_FIELD_MAX_LEVEL
		//PLAYER_FIELD_MAX_LEVEL : type = Int, size = 1, flag = Private
		public virtual UInt32 MaxLevel {
			get { return GetUInt32(UpdateFields.PLAYER_FIELD_MAX_LEVEL); }
			set { SetUInt32(UpdateFields.PLAYER_FIELD_MAX_LEVEL, value); }
		}
		#endregion

		//PLAYER_FIELD_DAILY_QUESTS_1 : type = Int, size = 25, flag = Private

		//PLAYER_RUNE_REGEN_1 : type = Single, size = 4, flag = Private

		//PLAYER_NO_REAGENT_COST_1 : type = Int, size = 3, flag = Private

		//PLAYER_FIELD_GLYPH_SLOTS_1 : type = Int, size = 8, flag = Private

		//PLAYER_FIELD_GLYPHS_1 : type = Int, size = 8, flag = Private

		#region PLAYER_GLYPHS_ENABLED
		//PLAYER_GLYPHS_ENABLED : type = Int, size = 1, flag = Private
		public virtual UInt32 GlyphsEnabled {
			get { return GetUInt32(UpdateFields.PLAYER_GLYPHS_ENABLED); }
			set { SetUInt32(UpdateFields.PLAYER_GLYPHS_ENABLED, value); }
		}
		#endregion
	}
}
