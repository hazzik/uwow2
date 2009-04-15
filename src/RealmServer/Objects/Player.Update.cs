using System;

namespace Hazzik.Objects {
	public partial class Player {
		#region PLAYER_DUEL_ARBITER

		//PLAYER_DUEL_ARBITER : type = Long, size = 2, flag = Public
		public virtual UInt64 DuelArbiterGuid { get; set; }

		#endregion

		#region PLAYER_FLAGS

		//PLAYER_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags { get; set; }

		#endregion

		#region PLAYER_GUILDID

		//PLAYER_GUILDID : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildId { get; set; }

		#endregion

		#region PLAYER_GUILDRANK

		//PLAYER_GUILDRANK : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildRank { get; set; }

		#endregion

		#region PLAYER_BYTES

		//PLAYER_BYTES : type = Bytes, size = 1, flag = Public
		public byte Skin { get; set; }

		public byte Face { get; set; }

		public byte HairStyle { get; set; }

		public byte HairColor { get; set; }

		#endregion

		#region PLAYER_BYTES_2

		//PLAYER_BYTES_2 : type = Bytes, size = 1, flag = Public
		public byte FacialHair { get; set; }

		public byte PlayerBytes2_2 { get; set; }

		public byte BankBagSlots { get; internal set; }

		public RestState RestState { get; set; }

		#endregion

		#region PLAYER_BYTES_3

		//PLAYER_BYTES_3 : type = Bytes, size = 1, flag = Public
		public override GenderType Gender {
			get { return base.Gender; }
			set { base.Gender = value; }
		}

		public virtual byte DrunkState { get; set; }

		public virtual byte PlayerBytes3_3 { get; set; }

		public virtual byte PvPRank { get; set; }

		#endregion

		#region PLAYER_DUEL_TEAM

		//PLAYER_DUEL_TEAM : type = Int, size = 1, flag = Public
		public virtual UInt32 DuelTeam { get; set; }

		#endregion

		#region PLAYER_GUILD_TIMESTAMP

		//PLAYER_GUILD_TIMESTAMP : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildTimestamp { get; set; }

		#endregion

		#region PLAYER_QUEST_LOG_1_1 * 25

		//PLAYER_QUEST_LOG_1_1 : type = Int, size = 1, flag = PartyMember
		//PLAYER_QUEST_LOG_1_2 : type = Int, size = 1, flag = Private
		//PLAYER_QUEST_LOG_1_3 : type = Bytes, size = 1, flag = Private
		//PLAYER_QUEST_LOG_1_4 : type = Int, size = 1, flag = Private

		#endregion

		#region PLAYER_VISIBLE_ITEM_1	* 19

		//PLAYER_VISIBLE_ITEM_1_ENTRYID : type = Int, size = 1, flag = Public
		//PLAYER_VISIBLE_ITEM_1_ENCHANTMENT : type = Shorts, size = 1, flag = Public

		#endregion

		#region PLAYER_CHOSEN_TITLE

		//PLAYER_CHOSEN_TITLE : type = Int, size = 1, flag = Public
		public virtual UInt32 ChosenTitle { get; set; }

		#endregion

		#region PLAYER_FARSIGHT

		//PLAYER_FARSIGHT : type = Long, size = 2, flag = Private
		public virtual UInt64 FarSightGuid { get; set; }

		#endregion

		#region PLAYER__FIELD_KNOWN_TITLES

		//PLAYER__FIELD_KNOWN_TITLES : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownTitlesGuid { get; set; }

		#endregion

		#region PLAYER__FIELD_KNOWN_TITLES1

		//PLAYER__FIELD_KNOWN_TITLES1 : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownTitles1Guid { get; set; }

		#endregion

		#region PLAYER__FIELD_KNOWN_TITLES2

		//PLAYER__FIELD_KNOWN_TITLES2 : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownTitles2Guid { get; set; }

		#endregion

		#region PLAYER_FIELD_KNOWN_CURRENCIES

		//PLAYER_FIELD_KNOWN_CURRENCIES : type = Long, size = 2, flag = Private
		public virtual UInt64 KnownCurrenciesGuid { get; set; }

		#endregion

		#region PLAYER_XP

		//PLAYER_XP : type = Int, size = 1, flag = Private
		public virtual UInt32 Xp { get; set; }

		#endregion

		#region PLAYER_NEXT_LEVEL_XP

		//PLAYER_NEXT_LEVEL_XP : type = Int, size = 1, flag = Private
		public virtual UInt32 NextLevelXp { get; set; }

		#endregion

		#region PLAYER_CHARACTER_POINTS1

		//PLAYER_CHARACTER_POINTS1 : type = Int, size = 1, flag = Private
		public virtual UInt32 CharacterPoints1 { get; set; }

		#endregion

		#region PLAYER_CHARACTER_POINTS2

		//PLAYER_CHARACTER_POINTS2 : type = Int, size = 1, flag = Private
		public virtual UInt32 CharacterPoints2 { get; set; }

		#endregion

		#region PLAYER_TRACK_CREATURES

		//PLAYER_TRACK_CREATURES : type = Int, size = 1, flag = Private
		public virtual UInt32 TrackCreatures { get; set; }

		#endregion

		#region PLAYER_TRACK_RESOURCES

		//PLAYER_TRACK_RESOURCES : type = Int, size = 1, flag = Private
		public virtual UInt32 TrackResources { get; set; }

		#endregion

		#region PLAYER_BLOCK_PERCENTAGE

		//PLAYER_BLOCK_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single BlockPercentage { get; set; }

		#endregion

		#region PLAYER_DODGE_PERCENTAGE

		//PLAYER_DODGE_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single DodgePercentage { get; set; }

		#endregion

		#region PLAYER_PARRY_PERCENTAGE

		//PLAYER_PARRY_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single ParryPercentage { get; set; }

		#endregion

		#region PLAYER_EXPERTISE

		//PLAYER_EXPERTISE : type = Int, size = 1, flag = Private
		public virtual UInt32 Expertise { get; set; }

		#endregion

		#region PLAYER_OFFHAND_EXPERTISE

		//PLAYER_OFFHAND_EXPERTISE : type = Int, size = 1, flag = Private
		public virtual UInt32 OffHandExpertise { get; set; }

		#endregion

		#region PLAYER_CRIT_PERCENTAGE

		//PLAYER_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single CritPercentage { get; set; }

		#endregion

		#region PLAYER_RANGED_CRIT_PERCENTAGE

		//PLAYER_RANGED_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single RangedCritPercentage { get; set; }

		#endregion

		#region PLAYER_OFFHAND_CRIT_PERCENTAGE

		//PLAYER_OFFHAND_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single OffHandCritPercentage { get; set; }

		#endregion

		#region PLAYER_SHIELD_BLOCK

		//PLAYER_SHIELD_BLOCK : type = Int, size = 1, flag = Private
		public virtual UInt32 ShieldBlock { get; set; }

		#endregion

		#region PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE

		//PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE : type = Single, size = 1, flag = Private
		public virtual Single ShieldBlockCritPercentage { get; set; }

		#endregion

		#region PLAYER_REST_STATE_EXPERIENCE

		//PLAYER_REST_STATE_EXPERIENCE : type = Int, size = 1, flag = Private
		public virtual UInt32 RestStateExperience { get; set; }

		#endregion

		#region PLAYER_FIELD_COINAGE

		//PLAYER_FIELD_COINAGE : type = Int, size = 1, flag = Private
		public virtual UInt32 Coinage { get; set; }

		#endregion

		#region PLAYER_FIELD_MOD_HEALING_DONE_POS

		//PLAYER_FIELD_MOD_HEALING_DONE_POS : type = Int, size = 1, flag = Private
		public virtual UInt32 ModHealingDonePos { get; set; }

		#endregion

		#region PLAYER_FIELD_MOD_TARGET_RESISTANCE

		//PLAYER_FIELD_MOD_TARGET_RESISTANCE : type = Int, size = 1, flag = Private
		public virtual UInt32 ModTargetResistance { get; set; }

		#endregion

		#region PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE

		//PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE : type = Int, size = 1, flag = Private
		public virtual UInt32 ModTargetPhysicalResistance { get; set; }

		#endregion

		#region PLAYER_FIELD_BYTES

		//PLAYER_FIELD_BYTES : type = Bytes, size = 1, flag = Private
		public virtual UInt32 FieldBytes { get; set; }

		#endregion

		#region PLAYER_AMMO_ID

		//PLAYER_AMMO_ID : type = Int, size = 1, flag = Private
		public virtual UInt32 AmmoId { get; set; }

		#endregion

		#region PLAYER_SELF_RES_SPELL

		//PLAYER_SELF_RES_SPELL : type = Int, size = 1, flag = Private
		public virtual UInt32 SelfResSpell { get; set; }

		#endregion

		#region PLAYER_FIELD_PVP_MEDALS

		//PLAYER_FIELD_PVP_MEDALS : type = Int, size = 1, flag = Private
		public virtual UInt32 PvpMedals { get; set; }

		#endregion

		#region PLAYER_FIELD_KILLS

		//PLAYER_FIELD_KILLS : type = Shorts, size = 1, flag = Private
		public virtual UInt32 Kills { get; set; }

		#endregion

		#region PLAYER_FIELD_TODAY_CONTRIBUTION

		//PLAYER_FIELD_TODAY_CONTRIBUTION : type = Int, size = 1, flag = Private
		public virtual UInt32 TodayContribution { get; set; }

		#endregion

		#region PLAYER_FIELD_YESTERDAY_CONTRIBUTION

		//PLAYER_FIELD_YESTERDAY_CONTRIBUTION : type = Int, size = 1, flag = Private
		public virtual UInt32 YesterdayContribution { get; set; }

		#endregion

		#region PLAYER_FIELD_LIFETIME_HONORBALE_KILLS

		//PLAYER_FIELD_LIFETIME_HONORBALE_KILLS : type = Int, size = 1, flag = Private
		public virtual UInt32 LifetimeHonorbaleKills { get; set; }

		#endregion

		#region PLAYER_FIELD_BYTES2

		//PLAYER_FIELD_BYTES2 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 FieldBytes2 { get; set; }

		#endregion

		#region PLAYER_FIELD_WATCHED_FACTION_INDEX

		//PLAYER_FIELD_WATCHED_FACTION_INDEX : type = Int, size = 1, flag = Private
		public virtual int WatchedFactionIndex { get; set; }

		#endregion

		#region PLAYER_FIELD_HONOR_CURRENCY

		//PLAYER_FIELD_HONOR_CURRENCY : type = Int, size = 1, flag = Private
		public virtual UInt32 HonorCurrency { get; set; }

		#endregion

		#region PLAYER_FIELD_ARENA_CURRENCY

		//PLAYER_FIELD_ARENA_CURRENCY : type = Int, size = 1, flag = Private
		public virtual UInt32 ArenaCurrency { get; set; }

		#endregion

		#region PLAYER_FIELD_MAX_LEVEL

		//PLAYER_FIELD_MAX_LEVEL : type = Int, size = 1, flag = Private
		public virtual UInt32 MaxLevel { get; set; }

		#endregion

		#region PLAYER_GLYPHS_ENABLED

		//PLAYER_GLYPHS_ENABLED : type = Int, size = 1, flag = Private
		public virtual UInt32 GlyphsEnabled { get; set; }

		#endregion

		//PLAYER_FIELD_PAD_0 : type = Int, size = 1, flag = None

		//PLAYER_FIELD_INV_SLOT_HEAD : type = Long, size = 46, flag = Private

		//PLAYER_FIELD_PACK_SLOT_1 : type = Long, size = 32, flag = Private

		//PLAYER_FIELD_BANK_SLOT_1 : type = Long, size = 56, flag = Private

		//PLAYER_FIELD_BANKBAG_SLOT_1 : type = Long, size = 14, flag = Private

		//PLAYER_FIELD_VENDORBUYBACK_SLOT_1 : type = Long, size = 24, flag = Private

		//PLAYER_FIELD_KEYRING_SLOT_1 : type = Long, size = 64, flag = Private

		//PLAYER_FIELD_CURRENCYTOKEN_SLOT_1 : type = Long, size = 64, flag = Private

		//PLAYER_SKILL_INFO_1_1 : type = Shorts, size = 384, flag = Private

		//PLAYER_SPELL_CRIT_PERCENTAGE1 : type = Single, size = 7, flag = Private

		//PLAYER_EXPLORED_ZONES_1 : type = Bytes, size = 128, flag = Private

		//PLAYER_FIELD_MOD_DAMAGE_DONE_POS : type = Int, size = 7, flag = Private

		//PLAYER_FIELD_MOD_DAMAGE_DONE_NEG : type = Int, size = 7, flag = Private

		//PLAYER_FIELD_MOD_DAMAGE_DONE_PCT : type = Int, size = 7, flag = Private

		//PLAYER_FIELD_BUYBACK_PRICE_1 : type = Int, size = 12, flag = Private

		//PLAYER_FIELD_BUYBACK_TIMESTAMP_1 : type = Int, size = 12, flag = Private

		//PLAYER_FIELD_COMBAT_RATING_1 : type = Int, size = 25, flag = Private

		//PLAYER_FIELD_ARENA_TEAM_INFO_1_1 : type = Int, size = 18, flag = Private

		//PLAYER_FIELD_DAILY_QUESTS_1 : type = Int, size = 25, flag = Private

		//PLAYER_RUNE_REGEN_1 : type = Single, size = 4, flag = Private

		//PLAYER_NO_REAGENT_COST_1 : type = Int, size = 3, flag = Private

		//PLAYER_FIELD_GLYPH_SLOTS_1 : type = Int, size = 8, flag = Private

		//PLAYER_FIELD_GLYPHS_1 : type = Int, size = 8, flag = Private
	}
}