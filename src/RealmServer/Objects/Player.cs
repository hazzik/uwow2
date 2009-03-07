using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Hazzik.Net;

namespace Hazzik.Objects {
	public partial class Player : Unit {
		private readonly IDictionary<ulong, ObjectUpdater> _objectUpdaters = new Dictionary<ulong, ObjectUpdater>();

		public Player()
			: base((int)UpdateFields.PLAYER_END, 0x19) {
			//Level = 1;

			MapId = 530;
			X = -3961.64F;
			Y = -13931.2F;
			Z = 100.615F;
			O = 2.083644F;

			Speed0 = 2.5F;
			Speed1 = 7F;
			Speed2 = 4.5F;
			Speed3 = 4.722222F;
			Speed4 = 2.5F;
			Speed5 = 7F;
			Speed6 = 4.5F;
			TurnRate = 3.141593F;

			UpdateValues[22].UInt32 = 0x0100010B; // 22	UNIT_FIELD_HEALTH
			UpdateValues[23].UInt32 = 0x00000032; // 23	UNIT_FIELD_POWER1
			UpdateValues[26].UInt32 = 0x00000064; // 26	UNIT_FIELD_POWER4
			UpdateValues[27].UInt32 = 0x00000064; // 27	UNIT_FIELD_POWER5
			UpdateValues[29].UInt32 = 0x00000008; // 29	UNIT_FIELD_POWER7
			UpdateValues[31].UInt32 = 0x00000032; // 31	UNIT_FIELD_MAXPOWER1
			UpdateValues[33].UInt32 = 0x000003E8; // 33	UNIT_FIELD_MAXPOWER3
			UpdateValues[34].UInt32 = 0x00000064; // 34	UNIT_FIELD_MAXPOWER4
			UpdateValues[35].UInt32 = 0x00000064; // 35	UNIT_FIELD_MAXPOWER5
			UpdateValues[37].UInt32 = 0x00000008; // 37	UNIT_FIELD_MAXPOWER7
			UpdateValues[38].UInt32 = 0x000003E8; // 38	UNIT_FIELD_LEVEL
			UpdateValues[53].UInt32 = 0x00000001; // 53	53
			UpdateValues[54].UInt32 = 0x0000065D; // 54	54
			UpdateValues[58].UInt32 = 0x00000008; // 58	58
			UpdateValues[59].UInt32 = 0x00000800; // 59	59
			UpdateValues[61].UInt32 = 0x00000B54; // 61	61
			UpdateValues[62].UInt32 = 0x000007D0; // 62	62
			UpdateValues[64].UInt32 = 0x3F800000; // 64	64
			UpdateValues[65].UInt32 = 0x3FC00000; // 65	65
			UpdateValues[66].UInt32 = 0x00003EFD; // 66	66
			UpdateValues[67].UInt32 = 0x00003EFD; // 67	67
			UpdateValues[69].UInt32 = 0x4116BE2C; // 69	69
			UpdateValues[70].UInt32 = 0x4136BE2C; // 70	70
			UpdateValues[80].UInt32 = 0x3F800000; // 80	80
			UpdateValues[84].UInt32 = 0x00000018; // 84	84
			UpdateValues[85].UInt32 = 0x00000011; // 85	85
			UpdateValues[86].UInt32 = 0x00000015; // 86	86
			UpdateValues[87].UInt32 = 0x00000015; // 87	87
			UpdateValues[88].UInt32 = 0x00000016; // 88	88
			UpdateValues[99].UInt32 = 0x00000024; // 99	99
			UpdateValues[121].UInt32 = 0x00000014; // 121	121
			UpdateValues[122].UInt32 = 0x11000000; // 122	UNIT_FIELD_AURALEVELS
			UpdateValues[123].UInt32 = 0x0000001F; // 123	123
			UpdateValues[126].UInt32 = 0x00000008; // 126	126
			UpdateValues[146].UInt32 = 0x3F800000; // 146	146
			UpdateValues[153].UInt32 = 0x02020505; // 153	UNIT_FIELD_RANGEDATTACKTIME
			UpdateValues[154].UInt32 = 0x02000003; // 154	UNIT_FIELD_BOUNDINGRADIUS
			UpdateValues[314].UInt32 = 0x00005BB1; // 314	PLAYER_QUEST_LOG_17_3
			UpdateValues[368].UInt32 = 0x00005BB2; // 368	PLAYER_VISIBLE_ITEM_2_0
			UpdateValues[386].UInt32 = 0x00005BB3; // 386	PLAYER_VISIBLE_ITEM_3_0
			UpdateValues[530].UInt32 = 0x00005B32; // 530	PLAYER_VISIBLE_ITEM_11_0
			//UpdateValues[608].UInt32 = 0xCC176E02; // 608	608
			//UpdateValues[609].UInt32 = 0x40000000; // 609	609
			//UpdateValues[614].UInt32 = 0xCC176E07; // 614	614
			//UpdateValues[615].UInt32 = 0x40000000; // 615	PLAYER_VISIBLE_ITEM_15_PROPERTIES
			//UpdateValues[616].UInt32 = 0xCC176E03; // 616	PLAYER_VISIBLE_ITEM_15_SEED
			//UpdateValues[617].UInt32 = 0x40000000; // 617	PLAYER_VISIBLE_ITEM_15_PAD
			//UpdateValues[632].UInt32 = 0xCC176E05; // 632	632
			//UpdateValues[633].UInt32 = 0x40000000; // 633	PLAYER_VISIBLE_ITEM_16_PROPERTIES
			//UpdateValues[648].UInt32 = 0xCC176E06; // 648	648
			//UpdateValues[649].UInt32 = 0x40000000; // 649	649
			//UpdateValues[650].UInt32 = 0xCC176E08; // 650	650
			//UpdateValues[651].UInt32 = 0x40000000; // 651	PLAYER_VISIBLE_ITEM_17_PROPERTIES
			UpdateValues[1008].UInt32 = 0x00001800; // 1008	1008
			UpdateValues[1009].UInt32 = 0x00000000; // 1009	1009
			UpdateValues[1011].UInt32 = 0x00000190; // 1011	1011
			UpdateValues[1012].UInt32 = 0x00000164; // 1012	1012
			UpdateValues[1015].UInt32 = 0x000000AB; // 1015	1015
			UpdateValues[1018].UInt32 = 0x000000AC; // 1018	1018
			UpdateValues[1019].UInt32 = 0x00050000; // 1019	1019
			UpdateValues[1021].UInt32 = 0x0000005F; // 1021	1021
			UpdateValues[1022].UInt32 = 0x00050001; // 1022	1022
			UpdateValues[1024].UInt32 = 0x000000A2; // 1024	1024
			UpdateValues[1025].UInt32 = 0x00050001; // 1025	1025
			UpdateValues[1027].UInt32 = 0x0000006F; // 1027	1027
			UpdateValues[1030].UInt32 = 0x00000071; // 1030	1030
			UpdateValues[1033].UInt32 = 0x00000073; // 1033	1033
			UpdateValues[1036].UInt32 = 0x000000AD; // 1036	1036
			UpdateValues[1037].UInt32 = 0x00050000; // 1037	1037
			UpdateValues[1039].UInt32 = 0x000000B7; // 1039	1039
			UpdateValues[1040].UInt32 = 0x00050005; // 1040	1040
			UpdateValues[1042].UInt32 = 0x000000B0; // 1042	1042
			UpdateValues[1043].UInt32 = 0x00050000; // 1043	1043
			UpdateValues[1045].UInt32 = 0x0000019F; // 1045	1045
			UpdateValues[1046].UInt32 = 0x00010001; // 1046	1046
			UpdateValues[1048].UInt32 = 0x00000100; // 1048	1048
			UpdateValues[1049].UInt32 = 0x00050000; // 1049	1049
			UpdateValues[1051].UInt32 = 0x00000189; // 1051	1051
			UpdateValues[1054].UInt32 = 0x00000089; // 1054	1054
			UpdateValues[1057].UInt32 = 0x000002F7; // 1057	1057
			UpdateValues[1058].UInt32 = 0x012C012C; // 1058	1058
			UpdateValues[1060].UInt32 = 0x000000B6; // 1060	1060
			UpdateValues[1063].UInt32 = 0x00000037; // 1063	1063
			UpdateValues[1064].UInt32 = 0x00050001; // 1064	1064
			UpdateValues[1066].UInt32 = 0x00000305; // 1066	1066
			UpdateValues[1069].UInt32 = 0x00000076; // 1069	1069
			UpdateValues[1072].UInt32 = 0x0000008A; // 1072	1072
			UpdateValues[1073].UInt32 = 0x012C0000; // 1073	1073
			UpdateValues[1075].UInt32 = 0x0000008D; // 1075	1075
			UpdateValues[1078].UInt32 = 0x00000081; // 1078	1078
			UpdateValues[1081].UInt32 = 0x000000A0; // 1081	1081
			UpdateValues[1082].UInt32 = 0x00050000; // 1082	1082
			UpdateValues[1084].UInt32 = 0x000000A5; // 1084	1084
			UpdateValues[1087].UInt32 = 0x0000002B; // 1087	1087
			UpdateValues[1088].UInt32 = 0x00050001; // 1088	1088
			UpdateValues[1090].UInt32 = 0x000001D9; // 1090	1090
			UpdateValues[1091].UInt32 = 0x00010000; // 1091	1091
			UpdateValues[1093].UInt32 = 0x0000002C; // 1093	1093
			UpdateValues[1094].UInt32 = 0x00050000; // 1094	PLAYER__FIELD_KNOWN_TITLES
			UpdateValues[1096].UInt32 = 0x000000A4; // 1096	PLAYER_FIELD_KNOWN_CURRENCIES
			UpdateValues[1099].UInt32 = 0x000000CA; // 1099	PLAYER_NEXT_LEVEL_XP
			UpdateValues[1102].UInt32 = 0x00000301; // 1102	1102
			UpdateValues[1103].UInt32 = 0x00050000; // 1103	1103
			UpdateValues[1105].UInt32 = 0x000000BA; // 1105	1105
			UpdateValues[1108].UInt32 = 0x000000E2; // 1108	1108
			UpdateValues[1109].UInt32 = 0x00050000; // 1109	1109
			UpdateValues[1111].UInt32 = 0x0000006D; // 1111	1111
			UpdateValues[1114].UInt32 = 0x000001B1; // 1114	1114
			UpdateValues[1115].UInt32 = 0x00010001; // 1115	1115
			UpdateValues[1117].UInt32 = 0x0000008C; // 1117	1117
			UpdateValues[1118].UInt32 = 0x012C0000; // 1118	1118
			UpdateValues[1120].UInt32 = 0x00000139; // 1120	1120
			UpdateValues[1123].UInt32 = 0x0000019D; // 1123	1123
			UpdateValues[1124].UInt32 = 0x00010001; // 1124	1124
			UpdateValues[1126].UInt32 = 0x0000019E; // 1126	1126
			UpdateValues[1127].UInt32 = 0x00010001; // 1127	1127
			UpdateValues[1129].UInt32 = 0x000000C5; // 1129	1129
			UpdateValues[1132].UInt32 = 0x0000014D; // 1132	1132
			UpdateValues[1135].UInt32 = 0x000002FA; // 1135	1135
			UpdateValues[1138].UInt32 = 0x0000008E; // 1138	1138
			UpdateValues[1139].UInt32 = 0x00010000; // 1139	1139
			UpdateValues[1141].UInt32 = 0x000002A1; // 1141	1141
			UpdateValues[1144].UInt32 = 0x000000CD; // 1144	1144
			UpdateValues[1145].UInt32 = 0x00010000; // 1145	1145
			UpdateValues[1147].UInt32 = 0x000000B9; // 1147	1147
			UpdateValues[1150].UInt32 = 0x000000E5; // 1150	1150
			UpdateValues[1153].UInt32 = 0x00000309; // 1153	1153
			UpdateValues[1154].UInt32 = 0x00010001; // 1154	1154
			UpdateValues[1156].UInt32 = 0x000002F3; // 1156	1156
			UpdateValues[1158].UInt32 = 0x00050000; // 1158	1158
			UpdateValues[1159].UInt32 = 0x000002F8; // 1159	1159
			UpdateValues[1160].UInt32 = 0x00050005; // 1160	1160
			UpdateValues[1162].UInt32 = 0x00000062; // 1162	1162
			UpdateValues[1163].UInt32 = 0x012C012C; // 1163	1163
			UpdateValues[1165].UInt32 = 0x00000088; // 1165	1165
			UpdateValues[1166].UInt32 = 0x00050000; // 1166	1166
			UpdateValues[1168].UInt32 = 0x00000036; // 1168	1168
			UpdateValues[1169].UInt32 = 0x00050001; // 1169	1169
			UpdateValues[1171].UInt32 = 0x0000002D; // 1171	1171
			UpdateValues[1172].UInt32 = 0x00050000; // 1172	1172
			UpdateValues[1174].UInt32 = 0x0000001A; // 1174	1174
			UpdateValues[1175].UInt32 = 0x00050005; // 1175	1175
			UpdateValues[1177].UInt32 = 0x0000030A; // 1177	1177
			UpdateValues[1178].UInt32 = 0x00010000; // 1178	1178
			UpdateValues[1180].UInt32 = 0x00000125; // 1180	1180
			UpdateValues[1183].UInt32 = 0x00000101; // 1183	1183
			UpdateValues[1184].UInt32 = 0x00050000; // 1184	1184
			UpdateValues[1186].UInt32 = 0x0000002E; // 1186	1186
			UpdateValues[1187].UInt32 = 0x00050000; // 1187	1187
			UpdateValues[1189].UInt32 = 0x0000013B; // 1189	1189
			UpdateValues[1192].UInt32 = 0x0000008B; // 1192	1192
			UpdateValues[1397].UInt32 = 0x00000002; // 1397	1397
			UpdateValues[1400].UInt32 = 0x409AE148; // 1400	1400
			UpdateValues[1401].UInt32 = 0x40E11F8B; // 1401	1401
			UpdateValues[1405].UInt32 = 0x40EDA9FB; // 1405	1405
			UpdateValues[1406].UInt32 = 0x40EDA9FB; // 1406	1406
			UpdateValues[1407].UInt32 = 0x40EDA9FB; // 1407	1407
			UpdateValues[1415].UInt32 = 0x00000002; // 1415	1415
			UpdateValues[1452].UInt32 = 0x40000000; // 1452	1452
			UpdateValues[1561].UInt32 = 0x3F800000; // 1561	1561
			UpdateValues[1562].UInt32 = 0x3F800000; // 1562	1562
			UpdateValues[1563].UInt32 = 0x3F800000; // 1563	1563
			UpdateValues[1564].UInt32 = 0x3F800000; // 1564	1564
			UpdateValues[1565].UInt32 = 0x3F800000; // 1565	1565
			UpdateValues[1566].UInt32 = 0x3F800000; // 1566	1566
			UpdateValues[1567].UInt32 = 0x3F800000; // 1567	1567
			UpdateValues[1571].UInt32 = 0x00000008; // 1571	1571
			UpdateValues[1504].UInt32 = 0xFFFFFFFF; // 1604	1604
			UpdateValues[1650].UInt32 = 0x00000050; // 1650	1650
			UpdateValues[1676].UInt32 = 0x3DCCCCCD; // 1676	1676
			UpdateValues[1677].UInt32 = 0x3DCCCCCD; // 1677	1677
			UpdateValues[1678].UInt32 = 0x3DCCCCCD; // 1678	1678
			UpdateValues[1679].UInt32 = 0x3DCCCCCD; // 1679	1679
			UpdateValues[1683].UInt32 = 0x00000015; // 1683	1683
			UpdateValues[1684].UInt32 = 0x00000016; // 1684	1684
			UpdateValues[1685].UInt32 = 0x00000017; // 1685	1685
			UpdateValues[1686].UInt32 = 0x00000018; // 1686	1686
			UpdateValues[1687].UInt32 = 0x00000019; // 1687	PLAYER_FIELD_KILLS
			UpdateValues[1688].UInt32 = 0x0000001A; // 1688	PLAYER_FIELD_TODAY_CONTRIBUTION

			AddSeenObject(this); // we always seen self
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Player; }
		}

		public void AddSeenObject(WorldObject obj) {
			if(!_objectUpdaters.ContainsKey(obj.Guid)) {
				_objectUpdaters.Add(obj.Guid, new ObjectUpdater(this, obj));
			}
		}

		public void RemoveObject(WorldObject obj) {
			_objectUpdaters.Remove(obj.Guid);
		}

		public BitArray GetRequeredMask(WorldObject obj) {
			var mask = new BitArray((int)UpdateFields.PLAYER_END);
			mask.SetAll(true);
			return mask;
		}

		public byte[] UpdateObjects() {
			using(var output = new MemoryStream()) {
				using(var writer = new BinaryWriter(output)) {
					writer.Write(_objectUpdaters.Count);
					foreach(var value in _objectUpdaters.Values) {
						value.WriteUpdate(writer);
					}
					return output.ToArray();
				}
			}
		}

		public string Name { get; set; }

		public byte skin;
		public byte face;
		public byte hairStyle;
		public byte hairColor;
		public byte facialHair;
		public byte RestState;
		public int PetDisplayId;
		public int PetLevel;
		public int PetCreatureFamily;
		public Item[] Items = new Item[20];
		public bool Dead;

		#region packets

		public IPacket GetLoginVerifyWorldPkt() {
			var result = new WorldPacket(WMSG.SMSG_LOGIN_VERIFY_WORLD);
			var w = result.CreateWriter();
			w.Write(MapId);
			w.Write(X);
			w.Write(Y);
			w.Write(Z);
			w.Write(O);
			return result;
		}

		public IPacket GetUpdateObjectPkt() {
			var r = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
			var w = r.CreateWriter();
			w.Write(UpdateObjects());
			return r;
		}

		public IPacket GetNameQueryResponcePkt() {
			var r = new WorldPacket(WMSG.SMSG_NAME_QUERY_RESPONSE);
			var w = r.CreateWriter();
			w.Write(Guid);
			w.WriteCString(Name);
			w.WriteCString("");
			w.Write((int)Race);
			w.Write((int)Gender);
			w.Write((int)Classe);
			w.Write((byte)01);
			w.WriteCString(Name);
			w.WriteCString(Name);
			w.WriteCString(Name);
			w.WriteCString(Name);
			w.WriteCString(Name);
			return r;
		}
		
		#endregion
	}
}