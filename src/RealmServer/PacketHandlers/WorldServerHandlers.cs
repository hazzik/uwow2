using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Net;
using Hazzik.Helper;
using Hazzik.Attributes;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	[PacketHandlerClass]
	public static class WorldServerHandlers {
		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleCMSG_REALM_SPLIT(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			var unk1 = r.ReadUInt32();

			var responce = new WorldPacket(WMSG.SMSG_REALM_SPLIT);
			var w = responce.CreateWriter();
			w.Write(unk1);
			//0-normal, 1-split, 2-split pending;
			w.Write(0);
			w.WriteCString(DateTime.Now.AddDays(1).ToShortDateString());
			w.WriteCString("hello");
			w.WriteCString("world");
			client.SendPacket(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
		public static void HandleCMSG_CHAR_ENUM(ClientBase client, IPacket packet) {
			try {
				var wclient = client as WorldClient;
				var p = new WorldPacket(WMSG.SMSG_CHAR_ENUM);
				var w = p.CreateWriter();
				w.Write((byte)wclient.Account.Players.Count());
				foreach(var player in wclient.Account.Players) {
					w.Write(player.Guid);
					w.WriteCString(player.Name);
					w.Write((byte)player.Race);
					w.Write((byte)player.Classe);
					w.Write((byte)player.Gender);
					w.Write(player.skin);
					w.Write(player.face);
					w.Write(player.hairStyle);
					w.Write(player.hairColor);
					w.Write(player.facialHair);
					w.Write((byte)player.level);

					w.Write((uint)player.ZoneId);
					w.Write((uint)player.MapId);
					w.Write(player.X);
					w.Write(player.Y);
					w.Write(player.Z);
					w.Write(player.GuildID);

					uint flag = 0x00000000;
					w.Write(flag);
					w.Write((byte)0);
					w.Write(player.PetDisplayId);
					w.Write(player.PetLevel);
					w.Write(player.PetCreatureFamily);
					for(int i = 0; i < 20; i++) {
						var item = player.Items[i];
						if(item != null) {
							w.Write(0);
							w.Write((byte)0);
							w.Write(0);
						}
						else {
							w.Write(0);
							w.Write((byte)0);
							w.Write(0);
						}
					}
				}
				client.SendPacket(p);
			} catch(Exception e) {
				Console.WriteLine(e.Message);
			}
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCMSG_CHAR_CREATE(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			var player = new Player() {
				Name = r.ReadCString(),
				Race = (Races)r.ReadByte(),
				Classe = (Classes)r.ReadByte(),
				Gender = r.ReadByte(),
				skin = r.ReadByte(),
				face = r.ReadByte(),
				hairStyle = r.ReadByte(),
				hairColor = r.ReadByte(),
				facialHair = r.ReadByte(),
			};
			(client as WorldClient).Account.AddPlayer(player);
			var responce = new WorldPacket(WMSG.SMSG_CHAR_CREATE);
			var w = responce.CreateWriter();
			w.Write((byte)44);
			client.SendPacket(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandleCMSG_PLAYER_LOGIN(ClientBase client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var player = (from players in (client as WorldClient).Account.Players
							  where players.Guid == guid
							  select players).FirstOrDefault();
			var r = (WorldPacket)null;
			var w = (BinaryWriter)null;
			if(null == player) {
				r = new WorldPacket(WMSG.SMSG_CHARACTER_LOGIN_FAILED);
				w = r.CreateWriter();
				w.Write((byte)0x44);
				client.SendPacket(r);
			}
			else {
				//pkt = 00022, 0x0236: SMSG_LOGIN_VERIFY_WORLD
				//0000: 36 02 01 00 00 00 27 A1 1A C4 5C DD 84 C5 3B DF : 6.....'...\...;.
				//0010: 1A 42 00 00 00 00 -- -- -- -- -- -- -- -- -- -- : .B....
				r = new WorldPacket(WMSG.SMSG_LOGIN_VERIFY_WORLD);
				w = r.CreateWriter();
				w.Write(1);
				w.Write(0F);
				w.Write(0F);
				w.Write(0F);
				w.Write(0);
				client.SendPacket(r);

				r = new WorldPacket(WMSG.SMSG_ACCOUNT_DATA_TIMES);
				w = r.CreateWriter();
				for(int i = 0; i < 0x80; i++) {
					w.Write((byte)0);
				}
				client.SendPacket(r);

				r = new WorldPacket(WMSG.SMSG_LOGIN_SETTIMESPEED);
				w = r.CreateWriter();
				w.Write(Program.GetActualTime());
				w.Write(0.01666667F);
				client.SendPacket(r);


				r = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
				w = r.CreateWriter();

				UpdateMgr mgr = new UpdateMgr();
				mgr.Add(player);
				var bytes = PacketsHelper.GetBytes(@"
pkt = 03104, 0x00A9: SMSG_UPDATE_OBJECT
0000: 01 00 00 00 00 02 8F 20 5B 78 FE 40 01 18 : ......... [x.@..
0010: 20 5B 78 FE 00 00 00 40 02 5F 41 00 00 00 00 00 :  [x....@._A.....
0020: 00 20 5B 78 FE 00 00 00 40 03 00 00 00 F6 17 00 : . [x....@.......
0030: 00 00 00 80 3F A7 9F B0 01 A7 9F B0 01 01 00 00 : ....?...........
0040: 00 02 8F 1D 5B 78 FE 40 01 18 1D 5B 78 FE 00 00 : ....[x.@...[x...
0050: 00 40 02 5F 41 00 00 00 00 00 0C 1D 5B 78 FE 00 : .@._A.......[x..
0060: 00 00 40 03 00 00 00 F7 17 00 00 00 00 80 3F A7 : ..@...........?.
0070: 9F B0 01 A7 9F B0 01 01 00 00 00 1E 00 00 00 1E : ................
0080: 00 00 00 02 8F 1C 5B 78 FE 40 01 18 1C 5B 78 FE : ......[x.@...[x.
0090: 00 00 00 40 02 5F 41 00 00 00 00 00 0C 1C 5B 78 : ...@._A.......[x
00A0: FE 00 00 00 40 03 00 00 00 24 00 00 00 00 00 80 : ....@....$......
00B0: 3F A7 9F B0 01 A7 9F B0 01 01 00 00 00 14 00 00 : ?...............
00C0: 00 14 00 00 00 02 8F 1B 5B 78 FE 40 01 18 1B 5B : ........[x.@...[
00D0: 78 FE 00 00 00 40 02 5F 41 01 00 00 00 00 00 1B : x....@._A.......
00E0: 5B 78 FE 00 00 00 40 03 00 00 00 75 00 00 00 00 : [x....@....u....
00F0: 00 80 3F A7 9F B0 01 A7 9F B0 01 04 00 00 00 FF : ..?.............
0100: FF FF FF 02 8F 1F 5B 78 FE 40 01 18 1F 5B 78 FE : ......[x.@...[x.
0110: 00 00 00 40 02 5F 41 01 00 00 00 00 00 1F 5B 78 : ...@._A.......[x
0120: FE 00 00 00 40 03 00 00 00 9F 00 00 00 00 00 80 : ....@...........
0130: 3F A7 9F B0 01 A7 9F B0 01 02 00 00 00 FF FF FF : ?...............
0140: FF 02 8F 1E 5B 78 FE 40 01 18 1E 5B 78 FE 00 00 : ....[x.@...[x...
0150: 00 40 02 5F 41 20 00 00 00 00 00 1E 5B 78 FE 00 : .@._A ......[x..
0160: 00 00 40 03 00 00 00 24 1B 00 00 00 00 80 3F A7 : ..@....$......?.
0170: 9F B0 01 A7 9F B0 01 01 00 00 00 01 00 00 00 03 : ................
0180: 0F A7 9F B0 01 04 71 00 00 00 00 00 82 5D 95 E6 : ......q......]..
0190: 27 A1 1A C4 5C DD 84 C5 3B DF 1A 42 00 00 00 00 : '...\...;..B....
01A0: 00 00 00 00 00 00 20 40 00 00 E0 40 00 00 90 40 : ...... @...@...@
01B0: 72 1C 97 40 00 00 20 40 00 00 E0 40 00 00 90 40 : r..@.. @...@...@
01C0: DB 0F 49 40 09 00 00 00 32 15 00 C0 F7 1D 40 00 : ..I@....2.....@.
01D0: 00 00 00 00 00 00 00 00 00 00 00 D8 1B 40 F8 00 : .............@..
01E0: 04 00 80 27 00 00 80 03 00 00 00 00 00 00 00 00 : ...'............
01F0: 00 00 00 00 00 00 00 00 00 00 04 00 00 00 00 00 : ................
0200: 04 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0210: 00 00 04 00 00 00 00 00 00 00 00 C3 00 00 03 00 : ................
0220: 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ?...............
0230: 00 00 00 00 00 00 00 00 00 00 00 00 80 59 96 2C : .............Y.,
0240: D9 B6 2D 49 92 64 4B 92 25 59 B2 64 C9 92 65 5B : ..-I.dK.%Y.d..e[
0250: 96 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : .,..............
0260: 00 00 00 00 00 00 00 00 00 00 00 00 00 32 FE 4F : .............2.O
0270: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0280: 00 F0 47 00 00 00 80 00 00 00 00 00 50 00 00 00 : ..G.........P...
0290: 00 A7 9F B0 01 19 00 00 00 00 00 80 3F 43 00 00 : ............?C..
02A0: 00 48 00 00 00 E8 03 00 00 64 00 00 00 64 00 00 : .H.......d...d..
02B0: 00 43 00 00 00 48 00 00 00 E8 03 00 00 64 00 00 : .C...H.......d..
02C0: 00 64 00 00 00 01 00 00 00 74 00 00 00 08 07 01 : .d.......t......
02D0: 00 08 00 00 00 6C 07 00 00 D0 07 00 00 08 AC 9C : .....l..........
02E0: 3E 00 00 C0 3F C7 05 00 00 C7 05 00 00 10 EA 90 : >...?...........
02F0: 40 10 EA D0 40 00 00 80 3F 16 00 00 00 16 00 00 : @...@...?.......
0300: 00 16 00 00 00 11 00 00 00 17 00 00 00 3A 00 00 : .............:..
0310: 00 37 00 00 00 1B 00 00 00 00 28 00 00 1A 00 00 : .7........(.....
0320: 00 0C 00 00 00 03 01 04 03 03 00 00 02 01 00 00 : ................
0330: 00 F6 17 00 00 F7 17 00 00 24 00 00 00 20 5B 78 : .........$... [x
0340: FE 00 00 00 40 1D 5B 78 FE 00 00 00 40 1C 5B 78 : ....@.[x....@.[x
0350: FE 00 00 00 40 1B 5B 78 FE 00 00 00 40 1F 5B 78 : ....@.[x....@.[x
0360: FE 00 00 00 40 1E 5B 78 FE 00 00 00 40 90 01 00 : ....@.[x....@...
0370: 00 4D 01 00 00 DD 02 00 00 05 00 05 00 B9 00 00 : .M..............
0380: 00 D9 01 00 00 00 00 01 00 71 00 00 00 F3 02 00 : .........q......
0390: 00 3B 01 00 00 2C 01 2C 01 8B 00 00 00 98 00 00 : .;...,.,........
03A0: 00 75 01 00 00 00 00 05 00 76 01 00 00 05 00 05 : .u.......v......
03B0: 00 77 01 00 00 05 00 05 00 9F 01 00 00 01 00 01 : .w..............
03C0: 00 6D 00 00 00 2C 01 2C 01 2C 00 00 00 00 00 05 : .m...,.,.,......
03D0: 00 89 00 00 00 8C 00 00 00 89 01 00 00 6F 00 00 : .............o..
03E0: 00 C9 02 00 00 95 00 00 00 94 00 00 00 A4 00 00 : ................
03F0: 00 AC 00 00 00 00 00 05 00 B7 00 00 00 05 00 05 : ................
0400: 00 64 01 00 00 B6 00 00 00 BA 00 00 00 62 00 00 : .d...........b..
0410: 00 88 00 00 00 01 00 05 00 15 02 00 00 2A 02 00 : .............*..
0420: 00 FA 02 00 00 AD 00 00 00 00 00 05 00 CA 00 00 : ................
0430: 00 AB 00 00 00 36 00 00 00 01 00 05 00 A1 02 00 : .....6..........
0440: 00 C5 00 00 00 5F 00 00 00 01 00 05 00 A5 00 00 : ....._..........
0450: 00 81 00 00 00 B1 01 00 00 01 00 01 00 39 01 00 : .............9..
0460: 00 9D 01 00 00 9E 01 00 00 01 00 01 00 F7 02 00 : ................
0470: 00 01 03 00 00 00 00 05 00 A0 00 00 00 00 00 05 : ................
0480: 00 A2 00 00 00 01 00 05 00 73 00 00 00 76 00 00 : .........s...v..
0490: 00 00 00 05 00 8A 00 00 00 8D 00 00 00 8E 00 00 : ................
04A0: 00 00 00 01 00 96 00 00 00 02 00 00 00 9A 99 99 : ................
04B0: 40 74 46 A4 40 21 8E A5 40 21 8E A5 40 21 8E A5 : @tF.@!..@!..@!..
04C0: 40 7C F2 8E 40 7C F2 8E 40 7C F2 8E 40 7C F2 8E : @|..@|..@|..@|..
04D0: 40 7C F2 8E 40 7C F2 8E 40 7C F2 8E 40 01 00 00 : @|..@|..@|..@...
04E0: 00 00 00 00 02 00 00 80 3F 00 00 80 3F 00 00 80 : ........?...?...
04F0: 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 : ?...?...?...?...
0500: 3F 08 00 00 00 FF FF FF FF C2 35 54 40 3C 00 00 : ?.........5T@<..
0510: 00 -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- : .
");

				w.Write(bytes);
				client.SendPacket(r);

				//r = new WorldPacket(WMSG.SMSG_TIME_SYNC_REQ);
				//w = r.GetWriter();

				//client.SendPacket(r);
			}
		}
	}
}
