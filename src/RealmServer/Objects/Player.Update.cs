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

		#region PLAYER_QUEST_LOG_1_1
		//PLAYER_QUEST_LOG_1_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog11 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_1_2
		//PLAYER_QUEST_LOG_1_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog12 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_1_3
		//PLAYER_QUEST_LOG_1_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog13 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_1_4
		//PLAYER_QUEST_LOG_1_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog14 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_1_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_2_1
		//PLAYER_QUEST_LOG_2_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog21 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_2_2
		//PLAYER_QUEST_LOG_2_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog22 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_2_3
		//PLAYER_QUEST_LOG_2_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog23 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_2_4
		//PLAYER_QUEST_LOG_2_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog24 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_2_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_3_1
		//PLAYER_QUEST_LOG_3_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog31 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_3_2
		//PLAYER_QUEST_LOG_3_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog32 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_3_3
		//PLAYER_QUEST_LOG_3_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog33 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_3_4
		//PLAYER_QUEST_LOG_3_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog34 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_3_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_4_1
		//PLAYER_QUEST_LOG_4_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog41 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_4_2
		//PLAYER_QUEST_LOG_4_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog42 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_4_3
		//PLAYER_QUEST_LOG_4_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog43 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_4_4
		//PLAYER_QUEST_LOG_4_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog44 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_4_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_5_1
		//PLAYER_QUEST_LOG_5_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog51 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_5_2
		//PLAYER_QUEST_LOG_5_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog52 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_5_3
		//PLAYER_QUEST_LOG_5_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog53 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_5_4
		//PLAYER_QUEST_LOG_5_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog54 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_5_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_6_1
		//PLAYER_QUEST_LOG_6_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog61 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_6_2
		//PLAYER_QUEST_LOG_6_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog62 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_6_3
		//PLAYER_QUEST_LOG_6_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog63 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_6_4
		//PLAYER_QUEST_LOG_6_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog64 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_6_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_7_1
		//PLAYER_QUEST_LOG_7_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog71 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_7_2
		//PLAYER_QUEST_LOG_7_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog72 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_7_3
		//PLAYER_QUEST_LOG_7_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog73 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_7_4
		//PLAYER_QUEST_LOG_7_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog74 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_7_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_8_1
		//PLAYER_QUEST_LOG_8_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog81 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_8_2
		//PLAYER_QUEST_LOG_8_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog82 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_8_3
		//PLAYER_QUEST_LOG_8_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog83 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_8_4
		//PLAYER_QUEST_LOG_8_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog84 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_8_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_9_1
		//PLAYER_QUEST_LOG_9_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog91 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_9_2
		//PLAYER_QUEST_LOG_9_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog92 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_9_3
		//PLAYER_QUEST_LOG_9_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog93 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_9_4
		//PLAYER_QUEST_LOG_9_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog94 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_9_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_10_1
		//PLAYER_QUEST_LOG_10_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog101 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_10_2
		//PLAYER_QUEST_LOG_10_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog102 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_10_3
		//PLAYER_QUEST_LOG_10_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog103 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_10_4
		//PLAYER_QUEST_LOG_10_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog104 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_10_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_11_1
		//PLAYER_QUEST_LOG_11_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog111 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_11_2
		//PLAYER_QUEST_LOG_11_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog112 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_11_3
		//PLAYER_QUEST_LOG_11_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog113 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_11_4
		//PLAYER_QUEST_LOG_11_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog114 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_11_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_12_1
		//PLAYER_QUEST_LOG_12_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog121 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_12_2
		//PLAYER_QUEST_LOG_12_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog122 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_12_3
		//PLAYER_QUEST_LOG_12_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog123 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_12_4
		//PLAYER_QUEST_LOG_12_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog124 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_12_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_13_1
		//PLAYER_QUEST_LOG_13_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog131 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_13_2
		//PLAYER_QUEST_LOG_13_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog132 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_13_3
		//PLAYER_QUEST_LOG_13_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog133 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_13_4
		//PLAYER_QUEST_LOG_13_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog134 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_13_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_14_1
		//PLAYER_QUEST_LOG_14_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog141 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_14_2
		//PLAYER_QUEST_LOG_14_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog142 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_14_3
		//PLAYER_QUEST_LOG_14_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog143 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_14_4
		//PLAYER_QUEST_LOG_14_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog144 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_14_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_15_1
		//PLAYER_QUEST_LOG_15_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog151 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_15_2
		//PLAYER_QUEST_LOG_15_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog152 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_15_3
		//PLAYER_QUEST_LOG_15_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog153 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_15_4
		//PLAYER_QUEST_LOG_15_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog154 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_15_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_16_1
		//PLAYER_QUEST_LOG_16_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog161 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_16_2
		//PLAYER_QUEST_LOG_16_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog162 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_16_3
		//PLAYER_QUEST_LOG_16_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog163 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_16_4
		//PLAYER_QUEST_LOG_16_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog164 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_16_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_17_1
		//PLAYER_QUEST_LOG_17_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog171 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_17_2
		//PLAYER_QUEST_LOG_17_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog172 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_17_3
		//PLAYER_QUEST_LOG_17_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog173 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_17_4
		//PLAYER_QUEST_LOG_17_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog174 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_17_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_18_1
		//PLAYER_QUEST_LOG_18_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog181 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_18_2
		//PLAYER_QUEST_LOG_18_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog182 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_18_3
		//PLAYER_QUEST_LOG_18_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog183 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_18_4
		//PLAYER_QUEST_LOG_18_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog184 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_18_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_19_1
		//PLAYER_QUEST_LOG_19_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog191 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_19_2
		//PLAYER_QUEST_LOG_19_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog192 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_19_3
		//PLAYER_QUEST_LOG_19_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog193 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_19_4
		//PLAYER_QUEST_LOG_19_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog194 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_19_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_20_1
		//PLAYER_QUEST_LOG_20_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog201 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_20_2
		//PLAYER_QUEST_LOG_20_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog202 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_20_3
		//PLAYER_QUEST_LOG_20_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog203 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_20_4
		//PLAYER_QUEST_LOG_20_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog204 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_20_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_21_1
		//PLAYER_QUEST_LOG_21_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog211 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_21_2
		//PLAYER_QUEST_LOG_21_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog212 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_21_3
		//PLAYER_QUEST_LOG_21_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog213 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_21_4
		//PLAYER_QUEST_LOG_21_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog214 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_21_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_22_1
		//PLAYER_QUEST_LOG_22_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog221 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_22_2
		//PLAYER_QUEST_LOG_22_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog222 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_22_3
		//PLAYER_QUEST_LOG_22_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog223 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_22_4
		//PLAYER_QUEST_LOG_22_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog224 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_22_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_23_1
		//PLAYER_QUEST_LOG_23_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog231 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_23_2
		//PLAYER_QUEST_LOG_23_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog232 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_23_3
		//PLAYER_QUEST_LOG_23_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog233 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_23_4
		//PLAYER_QUEST_LOG_23_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog234 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_23_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_24_1
		//PLAYER_QUEST_LOG_24_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog241 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_24_2
		//PLAYER_QUEST_LOG_24_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog242 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_24_3
		//PLAYER_QUEST_LOG_24_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog243 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_24_4
		//PLAYER_QUEST_LOG_24_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog244 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_24_4, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_25_1
		//PLAYER_QUEST_LOG_25_1 : type = Int, size = 1, flag = PartyMember
		public virtual UInt32 QuestLog251 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_1); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_1, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_25_2
		//PLAYER_QUEST_LOG_25_2 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog252 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_2); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_2, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_25_3
		//PLAYER_QUEST_LOG_25_3 : type = Bytes, size = 1, flag = Private
		public virtual UInt32 QuestLog253 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_3); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_3, value); }
		}
		#endregion

		#region PLAYER_QUEST_LOG_25_4
		//PLAYER_QUEST_LOG_25_4 : type = Int, size = 1, flag = Private
		public virtual UInt32 QuestLog254 {
			get { return GetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_4); }
			set { SetUInt32(UpdateFields.PLAYER_QUEST_LOG_25_4, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_1_CREATOR
		//PLAYER_VISIBLE_ITEM_1_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem1CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_1_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_1_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_1_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_1_PROPERTIES
		//PLAYER_VISIBLE_ITEM_1_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem1Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_1_SEED
		//PLAYER_VISIBLE_ITEM_1_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem1Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_1_PAD
		//PLAYER_VISIBLE_ITEM_1_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem1Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_1_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_2_CREATOR
		//PLAYER_VISIBLE_ITEM_2_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem2CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_2_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_2_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_2_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_2_PROPERTIES
		//PLAYER_VISIBLE_ITEM_2_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem2Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_2_SEED
		//PLAYER_VISIBLE_ITEM_2_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem2Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_2_PAD
		//PLAYER_VISIBLE_ITEM_2_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem2Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_2_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_3_CREATOR
		//PLAYER_VISIBLE_ITEM_3_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem3CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_3_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_3_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_3_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_3_PROPERTIES
		//PLAYER_VISIBLE_ITEM_3_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem3Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_3_SEED
		//PLAYER_VISIBLE_ITEM_3_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem3Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_3_PAD
		//PLAYER_VISIBLE_ITEM_3_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem3Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_3_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_4_CREATOR
		//PLAYER_VISIBLE_ITEM_4_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem4CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_4_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_4_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_4_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_4_PROPERTIES
		//PLAYER_VISIBLE_ITEM_4_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem4Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_4_SEED
		//PLAYER_VISIBLE_ITEM_4_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem4Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_4_PAD
		//PLAYER_VISIBLE_ITEM_4_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem4Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_4_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_5_CREATOR
		//PLAYER_VISIBLE_ITEM_5_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem5CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_5_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_5_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_5_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_5_PROPERTIES
		//PLAYER_VISIBLE_ITEM_5_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem5Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_5_SEED
		//PLAYER_VISIBLE_ITEM_5_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem5Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_5_PAD
		//PLAYER_VISIBLE_ITEM_5_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem5Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_5_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_6_CREATOR
		//PLAYER_VISIBLE_ITEM_6_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem6CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_6_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_6_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_6_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_6_PROPERTIES
		//PLAYER_VISIBLE_ITEM_6_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem6Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_6_SEED
		//PLAYER_VISIBLE_ITEM_6_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem6Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_6_PAD
		//PLAYER_VISIBLE_ITEM_6_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem6Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_6_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_7_CREATOR
		//PLAYER_VISIBLE_ITEM_7_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem7CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_7_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_7_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_7_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_7_PROPERTIES
		//PLAYER_VISIBLE_ITEM_7_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem7Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_7_SEED
		//PLAYER_VISIBLE_ITEM_7_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem7Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_7_PAD
		//PLAYER_VISIBLE_ITEM_7_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem7Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_7_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_8_CREATOR
		//PLAYER_VISIBLE_ITEM_8_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem8CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_8_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_8_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_8_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_8_PROPERTIES
		//PLAYER_VISIBLE_ITEM_8_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem8Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_8_SEED
		//PLAYER_VISIBLE_ITEM_8_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem8Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_8_PAD
		//PLAYER_VISIBLE_ITEM_8_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem8Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_8_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_9_CREATOR
		//PLAYER_VISIBLE_ITEM_9_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem9CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_9_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_9_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_9_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_9_PROPERTIES
		//PLAYER_VISIBLE_ITEM_9_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem9Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_9_SEED
		//PLAYER_VISIBLE_ITEM_9_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem9Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_9_PAD
		//PLAYER_VISIBLE_ITEM_9_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem9Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_9_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_10_CREATOR
		//PLAYER_VISIBLE_ITEM_10_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem10CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_10_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_10_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_10_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_10_PROPERTIES
		//PLAYER_VISIBLE_ITEM_10_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem10Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_10_SEED
		//PLAYER_VISIBLE_ITEM_10_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem10Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_10_PAD
		//PLAYER_VISIBLE_ITEM_10_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem10Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_10_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_11_CREATOR
		//PLAYER_VISIBLE_ITEM_11_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem11CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_11_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_11_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_11_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_11_PROPERTIES
		//PLAYER_VISIBLE_ITEM_11_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem11Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_11_SEED
		//PLAYER_VISIBLE_ITEM_11_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem11Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_11_PAD
		//PLAYER_VISIBLE_ITEM_11_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem11Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_11_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_12_CREATOR
		//PLAYER_VISIBLE_ITEM_12_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem12CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_12_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_12_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_12_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_12_PROPERTIES
		//PLAYER_VISIBLE_ITEM_12_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem12Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_12_SEED
		//PLAYER_VISIBLE_ITEM_12_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem12Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_12_PAD
		//PLAYER_VISIBLE_ITEM_12_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem12Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_12_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_13_CREATOR
		//PLAYER_VISIBLE_ITEM_13_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem13CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_13_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_13_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_13_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_13_PROPERTIES
		//PLAYER_VISIBLE_ITEM_13_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem13Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_13_SEED
		//PLAYER_VISIBLE_ITEM_13_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem13Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_13_PAD
		//PLAYER_VISIBLE_ITEM_13_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem13Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_13_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_14_CREATOR
		//PLAYER_VISIBLE_ITEM_14_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem14CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_14_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_14_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_14_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_14_PROPERTIES
		//PLAYER_VISIBLE_ITEM_14_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem14Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_14_SEED
		//PLAYER_VISIBLE_ITEM_14_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem14Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_14_PAD
		//PLAYER_VISIBLE_ITEM_14_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem14Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_14_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_15_CREATOR
		//PLAYER_VISIBLE_ITEM_15_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem15CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_15_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_15_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_15_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_15_PROPERTIES
		//PLAYER_VISIBLE_ITEM_15_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem15Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_15_SEED
		//PLAYER_VISIBLE_ITEM_15_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem15Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_15_PAD
		//PLAYER_VISIBLE_ITEM_15_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem15Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_15_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_16_CREATOR
		//PLAYER_VISIBLE_ITEM_16_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem16CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_16_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_16_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_16_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_16_PROPERTIES
		//PLAYER_VISIBLE_ITEM_16_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem16Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_16_SEED
		//PLAYER_VISIBLE_ITEM_16_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem16Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_16_PAD
		//PLAYER_VISIBLE_ITEM_16_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem16Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_16_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_17_CREATOR
		//PLAYER_VISIBLE_ITEM_17_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem17CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_17_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_17_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_17_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_17_PROPERTIES
		//PLAYER_VISIBLE_ITEM_17_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem17Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_17_SEED
		//PLAYER_VISIBLE_ITEM_17_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem17Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_17_PAD
		//PLAYER_VISIBLE_ITEM_17_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem17Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_17_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_18_CREATOR
		//PLAYER_VISIBLE_ITEM_18_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem18CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_18_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_18_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_18_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_18_PROPERTIES
		//PLAYER_VISIBLE_ITEM_18_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem18Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_18_SEED
		//PLAYER_VISIBLE_ITEM_18_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem18Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_18_PAD
		//PLAYER_VISIBLE_ITEM_18_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem18Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_18_PAD, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_19_CREATOR
		//PLAYER_VISIBLE_ITEM_19_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 VisibleItem19CreatorGuid {
			get { return GetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_19_CREATOR); }
			set { SetUInt64(UpdateFields.PLAYER_VISIBLE_ITEM_19_CREATOR, value); }
		}
		#endregion

		//PLAYER_VISIBLE_ITEM_19_0 : type = Int, size = 13, flag = Public

		#region PLAYER_VISIBLE_ITEM_19_PROPERTIES
		//PLAYER_VISIBLE_ITEM_19_PROPERTIES : type = Shorts, size = 1, flag = Public
		public virtual UInt32 VisibleItem19Properties {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_PROPERTIES); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_PROPERTIES, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_19_SEED
		//PLAYER_VISIBLE_ITEM_19_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem19Seed {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_SEED); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_SEED, value); }
		}
		#endregion

		#region PLAYER_VISIBLE_ITEM_19_PAD
		//PLAYER_VISIBLE_ITEM_19_PAD : type = Int, size = 1, flag = Public
		public virtual UInt32 VisibleItem19Pad {
			get { return GetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_PAD); }
			set { SetUInt32(UpdateFields.PLAYER_VISIBLE_ITEM_19_PAD, value); }
		}
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
