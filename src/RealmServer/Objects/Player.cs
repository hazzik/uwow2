using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Map;
using Hazzik.Net;

namespace Hazzik.Objects {
	public partial class Player : Unit {
		private readonly IDictionary<ulong, IUpdateBuilder> _updateBuilders = new Dictionary<ulong, IUpdateBuilder>();
		private readonly Timer2 _updateTimer;
		public bool Dead;
		public Item[] Items = new Item[20];
		public int PetCreatureFamily;
		public int PetDisplayId;
		public int PetLevel;

		public Player()
			: base((int)UpdateFields.PLAYER_END) {
			_updateTimer = new UpdateTimer(this);

			Type |= ObjectTypes.Player;

			InitFake();
			ObjectManager.Add(Corpse.Create(this));
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Player; }
		}

		public string Name { get; set; }

		public override StandStates StandState {
			get { return base.StandState; }
			set {
				if(value == base.StandState) {
					return;
				}
				base.StandState = value;
				Client.Send(new WorldPacket(WMSG.SMSG_STANDSTATE_UPDATE, new[] { (byte)value }));
			}
		}

		public WorldClient Client { get; protected internal set; }

		private void InitFake() {
			MapId = 530;
			PosX = -3961.64F;
			PosY = -13931.2F;
			PosZ = 100.615F;
			Facing = 2.083644F;

			Speed0 = 2.5F;
			Speed1 = 7F;
			Speed2 = 4.5F;
			Speed3 = 4.722222F;
			Speed4 = 2.5F;
			Speed5 = 7F;
			Speed6 = 4.5F;
			TurnRate = 3.141593F;

			SetUInt32((UpdateFields)22, 0x0100010B); // 22	UNIT_FIELD_HEALTH
			SetUInt32((UpdateFields)23, 0x00000032); // 23	UNIT_FIELD_POWER1
			SetUInt32((UpdateFields)26, 0x00000064); // 26	UNIT_FIELD_POWER4
			SetUInt32((UpdateFields)27, 0x00000064); // 27	UNIT_FIELD_POWER5
			SetUInt32((UpdateFields)29, 0x00000008); // 29	UNIT_FIELD_POWER7
			SetUInt32((UpdateFields)31, 0x00000032); // 31	UNIT_FIELD_MAXPOWER1
			SetUInt32((UpdateFields)33, 0x000003E8); // 33	UNIT_FIELD_MAXPOWER3
			SetUInt32((UpdateFields)34, 0x00000064); // 34	UNIT_FIELD_MAXPOWER4
			SetUInt32((UpdateFields)35, 0x00000064); // 35	UNIT_FIELD_MAXPOWER5
			SetUInt32((UpdateFields)37, 0x00000008); // 37	UNIT_FIELD_MAXPOWER7
			SetUInt32((UpdateFields)38, 0x000003E8); // 38	UNIT_FIELD_LEVEL
			SetUInt32((UpdateFields)53, 0x00000001); // 53	53
			SetUInt32((UpdateFields)54, 0x0000065D); // 54	54
			SetUInt32((UpdateFields)58, 0x00000008); // 58	58
			SetUInt32((UpdateFields)59, 0x00000800); // 59	59
			SetUInt32((UpdateFields)61, 0x00000B54); // 61	61
			SetUInt32((UpdateFields)62, 0x000007D0); // 62	62
			SetUInt32((UpdateFields)64, 0x3F800000); // 64	64
			SetUInt32((UpdateFields)65, 0x3FC00000); // 65	65
			SetUInt32((UpdateFields)66, 0x00003EFD); // 66	66
			SetUInt32((UpdateFields)67, 0x00003EFD); // 67	67
			SetUInt32((UpdateFields)69, 0x4116BE2C); // 69	69
			SetUInt32((UpdateFields)70, 0x4136BE2C); // 70	70
			SetUInt32((UpdateFields)80, 0x3F800000); // 80	80
			SetUInt32((UpdateFields)84, 0x00000018); // 84	84
			SetUInt32((UpdateFields)85, 0x00000011); // 85	85
			SetUInt32((UpdateFields)86, 0x00000015); // 86	86
			SetUInt32((UpdateFields)87, 0x00000015); // 87	87
			SetUInt32((UpdateFields)88, 0x00000016); // 88	88
			SetUInt32((UpdateFields)99, 0x00000024); // 99	99
			SetUInt32((UpdateFields)121, 0x00000014); // 121	121
			SetUInt32((UpdateFields)122, 0x11000000); // 122	UNIT_FIELD_AURALEVELS
			SetUInt32((UpdateFields)123, 0x0000001F); // 123	123
			SetUInt32((UpdateFields)126, 0x00000008); // 126	126
			SetUInt32((UpdateFields)146, 0x3F800000); // 146	146
			SetUInt32((UpdateFields)153, 0x02020505); // 153	UNIT_FIELD_RANGEDATTACKTIME
			SetUInt32((UpdateFields)154, 0x02000003); // 154	UNIT_FIELD_BOUNDINGRADIUS
			SetUInt32((UpdateFields)314, 0x00005BB1); // 314	PLAYER_QUEST_LOG_17_3
			SetUInt32((UpdateFields)368, 0x00005BB2); // 368	PLAYER_VISIBLE_ITEM_2_0
			SetUInt32((UpdateFields)386, 0x00005BB3); // 386	PLAYER_VISIBLE_ITEM_3_0
			SetUInt32((UpdateFields)530, 0x00005B32); // 530	PLAYER_VISIBLE_ITEM_11_0
			//SetUInt32((UpdateFields)608, 0xCC176E02); // 608	608
			//SetUInt32((UpdateFields)609, 0x40000000); // 609	609
			//SetUInt32((UpdateFields)614, 0xCC176E07); // 614	614
			//SetUInt32((UpdateFields)615, 0x40000000); // 615	PLAYER_VISIBLE_ITEM_15_PROPERTIES
			//SetUInt32((UpdateFields)616, 0xCC176E03); // 616	PLAYER_VISIBLE_ITEM_15_SEED
			//SetUInt32((UpdateFields)617, 0x40000000); // 617	PLAYER_VISIBLE_ITEM_15_PAD
			//SetUInt32((UpdateFields)632, 0xCC176E05); // 632	632
			//SetUInt32((UpdateFields)633, 0x40000000); // 633	PLAYER_VISIBLE_ITEM_16_PROPERTIES
			//SetUInt32((UpdateFields)648, 0xCC176E06); // 648	648
			//SetUInt32((UpdateFields)649, 0x40000000); // 649	649
			//SetUInt32((UpdateFields)650, 0xCC176E08); // 650	650
			//SetUInt32((UpdateFields)651, 0x40000000); // 651	PLAYER_VISIBLE_ITEM_17_PROPERTIES
			SetUInt32((UpdateFields)1008, 0x00001800); // 1008	1008
			SetUInt32((UpdateFields)1009, 0x00000000); // 1009	1009
			SetUInt32((UpdateFields)1011, 0x00000190); // 1011	1011
			SetUInt32((UpdateFields)1012, 0x00000164); // 1012	1012
			SetUInt32((UpdateFields)1015, 0x000000AB); // 1015	1015
			SetUInt32((UpdateFields)1018, 0x000000AC); // 1018	1018
			SetUInt32((UpdateFields)1019, 0x00050000); // 1019	1019
			SetUInt32((UpdateFields)1021, 0x0000005F); // 1021	1021
			SetUInt32((UpdateFields)1022, 0x00050001); // 1022	1022
			SetUInt32((UpdateFields)1024, 0x000000A2); // 1024	1024
			SetUInt32((UpdateFields)1025, 0x00050001); // 1025	1025
			SetUInt32((UpdateFields)1027, 0x0000006F); // 1027	1027
			SetUInt32((UpdateFields)1030, 0x00000071); // 1030	1030
			SetUInt32((UpdateFields)1033, 0x00000073); // 1033	1033
			SetUInt32((UpdateFields)1036, 0x000000AD); // 1036	1036
			SetUInt32((UpdateFields)1037, 0x00050000); // 1037	1037
			SetUInt32((UpdateFields)1039, 0x000000B7); // 1039	1039
			SetUInt32((UpdateFields)1040, 0x00050005); // 1040	1040
			SetUInt32((UpdateFields)1042, 0x000000B0); // 1042	1042
			SetUInt32((UpdateFields)1043, 0x00050000); // 1043	1043
			SetUInt32((UpdateFields)1045, 0x0000019F); // 1045	1045
			SetUInt32((UpdateFields)1046, 0x00010001); // 1046	1046
			SetUInt32((UpdateFields)1048, 0x00000100); // 1048	1048
			SetUInt32((UpdateFields)1049, 0x00050000); // 1049	1049
			SetUInt32((UpdateFields)1051, 0x00000189); // 1051	1051
			SetUInt32((UpdateFields)1054, 0x00000089); // 1054	1054
			SetUInt32((UpdateFields)1057, 0x000002F7); // 1057	1057
			SetUInt32((UpdateFields)1058, 0x012C012C); // 1058	1058
			SetUInt32((UpdateFields)1060, 0x000000B6); // 1060	1060
			SetUInt32((UpdateFields)1063, 0x00000037); // 1063	1063
			SetUInt32((UpdateFields)1064, 0x00050001); // 1064	1064
			SetUInt32((UpdateFields)1066, 0x00000305); // 1066	1066
			SetUInt32((UpdateFields)1069, 0x00000076); // 1069	1069
			SetUInt32((UpdateFields)1072, 0x0000008A); // 1072	1072
			SetUInt32((UpdateFields)1073, 0x012C0000); // 1073	1073
			SetUInt32((UpdateFields)1075, 0x0000008D); // 1075	1075
			SetUInt32((UpdateFields)1078, 0x00000081); // 1078	1078
			SetUInt32((UpdateFields)1081, 0x000000A0); // 1081	1081
			SetUInt32((UpdateFields)1082, 0x00050000); // 1082	1082
			SetUInt32((UpdateFields)1084, 0x000000A5); // 1084	1084
			SetUInt32((UpdateFields)1087, 0x0000002B); // 1087	1087
			SetUInt32((UpdateFields)1088, 0x00050001); // 1088	1088
			SetUInt32((UpdateFields)1090, 0x000001D9); // 1090	1090
			SetUInt32((UpdateFields)1091, 0x00010000); // 1091	1091
			SetUInt32((UpdateFields)1093, 0x0000002C); // 1093	1093
			SetUInt32((UpdateFields)1094, 0x00050000); // 1094	PLAYER__FIELD_KNOWN_TITLES
			SetUInt32((UpdateFields)1096, 0x000000A4); // 1096	PLAYER_FIELD_KNOWN_CURRENCIES
			SetUInt32((UpdateFields)1099, 0x000000CA); // 1099	PLAYER_NEXT_LEVEL_XP
			SetUInt32((UpdateFields)1102, 0x00000301); // 1102	1102
			SetUInt32((UpdateFields)1103, 0x00050000); // 1103	1103
			SetUInt32((UpdateFields)1105, 0x000000BA); // 1105	1105
			SetUInt32((UpdateFields)1108, 0x000000E2); // 1108	1108
			SetUInt32((UpdateFields)1109, 0x00050000); // 1109	1109
			SetUInt32((UpdateFields)1111, 0x0000006D); // 1111	1111
			SetUInt32((UpdateFields)1114, 0x000001B1); // 1114	1114
			SetUInt32((UpdateFields)1115, 0x00010001); // 1115	1115
			SetUInt32((UpdateFields)1117, 0x0000008C); // 1117	1117
			SetUInt32((UpdateFields)1118, 0x012C0000); // 1118	1118
			SetUInt32((UpdateFields)1120, 0x00000139); // 1120	1120
			SetUInt32((UpdateFields)1123, 0x0000019D); // 1123	1123
			SetUInt32((UpdateFields)1124, 0x00010001); // 1124	1124
			SetUInt32((UpdateFields)1126, 0x0000019E); // 1126	1126
			SetUInt32((UpdateFields)1127, 0x00010001); // 1127	1127
			SetUInt32((UpdateFields)1129, 0x000000C5); // 1129	1129
			SetUInt32((UpdateFields)1132, 0x0000014D); // 1132	1132
			SetUInt32((UpdateFields)1135, 0x000002FA); // 1135	1135
			SetUInt32((UpdateFields)1138, 0x0000008E); // 1138	1138
			SetUInt32((UpdateFields)1139, 0x00010000); // 1139	1139
			SetUInt32((UpdateFields)1141, 0x000002A1); // 1141	1141
			SetUInt32((UpdateFields)1144, 0x000000CD); // 1144	1144
			SetUInt32((UpdateFields)1145, 0x00010000); // 1145	1145
			SetUInt32((UpdateFields)1147, 0x000000B9); // 1147	1147
			SetUInt32((UpdateFields)1150, 0x000000E5); // 1150	1150
			SetUInt32((UpdateFields)1153, 0x00000309); // 1153	1153
			SetUInt32((UpdateFields)1154, 0x00010001); // 1154	1154
			SetUInt32((UpdateFields)1156, 0x000002F3); // 1156	1156
			SetUInt32((UpdateFields)1158, 0x00050000); // 1158	1158
			SetUInt32((UpdateFields)1159, 0x000002F8); // 1159	1159
			SetUInt32((UpdateFields)1160, 0x00050005); // 1160	1160
			SetUInt32((UpdateFields)1162, 0x00000062); // 1162	1162
			SetUInt32((UpdateFields)1163, 0x012C012C); // 1163	1163
			SetUInt32((UpdateFields)1165, 0x00000088); // 1165	1165
			SetUInt32((UpdateFields)1166, 0x00050000); // 1166	1166
			SetUInt32((UpdateFields)1168, 0x00000036); // 1168	1168
			SetUInt32((UpdateFields)1169, 0x00050001); // 1169	1169
			SetUInt32((UpdateFields)1171, 0x0000002D); // 1171	1171
			SetUInt32((UpdateFields)1172, 0x00050000); // 1172	1172
			SetUInt32((UpdateFields)1174, 0x0000001A); // 1174	1174
			SetUInt32((UpdateFields)1175, 0x00050005); // 1175	1175
			SetUInt32((UpdateFields)1177, 0x0000030A); // 1177	1177
			SetUInt32((UpdateFields)1178, 0x00010000); // 1178	1178
			SetUInt32((UpdateFields)1180, 0x00000125); // 1180	1180
			SetUInt32((UpdateFields)1183, 0x00000101); // 1183	1183
			SetUInt32((UpdateFields)1184, 0x00050000); // 1184	1184
			SetUInt32((UpdateFields)1186, 0x0000002E); // 1186	1186
			SetUInt32((UpdateFields)1187, 0x00050000); // 1187	1187
			SetUInt32((UpdateFields)1189, 0x0000013B); // 1189	1189
			SetUInt32((UpdateFields)1192, 0x0000008B); // 1192	1192
			SetUInt32((UpdateFields)1397, 0x00000002); // 1397	1397
			SetUInt32((UpdateFields)1400, 0x409AE148); // 1400	1400
			SetUInt32((UpdateFields)1401, 0x40E11F8B); // 1401	1401
			SetUInt32((UpdateFields)1405, 0x40EDA9FB); // 1405	1405
			SetUInt32((UpdateFields)1406, 0x40EDA9FB); // 1406	1406
			SetUInt32((UpdateFields)1407, 0x40EDA9FB); // 1407	1407
			SetUInt32((UpdateFields)1415, 0x00000002); // 1415	1415
			SetUInt32((UpdateFields)1452, 0x40000000); // 1452	1452
			SetUInt32((UpdateFields)1561, 0x3F800000); // 1561	1561
			SetUInt32((UpdateFields)1562, 0x3F800000); // 1562	1562
			SetUInt32((UpdateFields)1563, 0x3F800000); // 1563	1563
			SetUInt32((UpdateFields)1564, 0x3F800000); // 1564	1564
			SetUInt32((UpdateFields)1565, 0x3F800000); // 1565	1565
			SetUInt32((UpdateFields)1566, 0x3F800000); // 1566	1566
			SetUInt32((UpdateFields)1567, 0x3F800000); // 1567	1567
			SetUInt32((UpdateFields)1571, 0x00000008); // 1571	1571
			SetUInt32((UpdateFields)1504, 0xFFFFFFFF); // 1604	1604
			SetUInt32((UpdateFields)1650, 0x00000050); // 1650	1650
			SetUInt32((UpdateFields)1676, 0x3DCCCCCD); // 1676	1676
			SetUInt32((UpdateFields)1677, 0x3DCCCCCD); // 1677	1677
			SetUInt32((UpdateFields)1678, 0x3DCCCCCD); // 1678	1678
			SetUInt32((UpdateFields)1679, 0x3DCCCCCD); // 1679	1679
			SetUInt32((UpdateFields)1683, 0x00000015); // 1683	1683
			SetUInt32((UpdateFields)1684, 0x00000016); // 1684	1684
			SetUInt32((UpdateFields)1685, 0x00000017); // 1685	1685
			SetUInt32((UpdateFields)1686, 0x00000018); // 1686	1686
			SetUInt32((UpdateFields)1687, 0x00000019); // 1687	PLAYER_FIELD_KILLS
			SetUInt32((UpdateFields)1688, 0x0000001A); // 1688	PLAYER_FIELD_TODAY_CONTRIBUTION
		}

		public BitArray GetRequeredMask(WorldObject obj) {
			var mask = new BitArray((int)UpdateFields.PLAYER_END);
			mask.SetAll(true);
			return mask;
		}

		public void UpdateObjects() {
			var updateBuilders = GetUpdateBuilders();
			if(updateBuilders.Count != 0) {
				Client.Send(GetUpdateObjectPkt(updateBuilders));
			}
		}

		private ICollection<IUpdateBuilder> GetUpdateBuilders() {
			var items = Items.Cast<WorldObject>();
			var seenObjects = ObjectManager.GetSeenObjectsNear(this).Cast<WorldObject>();
			var objects = items.Concat(seenObjects);

			var notInRange = _updateBuilders.Keys.ToList();
			foreach(var obj in objects) {
				AddUpdateObject(obj);
				notInRange.Remove(obj.Guid);
			}
			foreach(var guid in notInRange) {
				_updateBuilders.Remove(guid);
			}
			var updateBuilders = _updateBuilders.Values.ToList();
			updateBuilders.Add(new OutOfRangeUpdater(notInRange));
			return updateBuilders.Where(x => x.IsChanged).ToList(); // except not changed
		}

		private void AddUpdateObject(WorldObject obj) {
			if(!_updateBuilders.ContainsKey(obj.Guid)) {
				_updateBuilders.Add(obj.Guid, new ObjectUpdater(this, obj));
			}
		}

		public void StartUpdateTimer() {
			_updateTimer.Start();
		}

		#region Nested type: UpdateTimer

		private class UpdateTimer : Timer2 {
			private readonly Player _player;

			public UpdateTimer(Player player)
				: base(3000) {
				_player = player;
			}

			public override void OnTick() {
				if(_player == null) {
					Stop();
					return;
				}
				_player.UpdateObjects();
			}
		}

		#endregion
	}
}