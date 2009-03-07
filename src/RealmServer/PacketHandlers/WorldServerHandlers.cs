using System;
using System.IO;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.Helper;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	[PacketHandlerClass]
	public static class WorldServerHandlers {
		[WorldPacketHandler(WMSG.CMSG_NAME_QUERY)]
		public static void HandleCMSG_NAME_QUERY(ClientBase client, IPacket packet) {
			var w = packet.CreateReader();
			var guid = w.ReadUInt32();
			//var player = from players in wclient.
		}

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
			client.Send(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
		public static void HandleCMSG_CHAR_ENUM(ClientBase client, IPacket packet) {
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
				w.Write((byte)player.Level);

				w.Write(player.ZoneId);
				w.Write(player.MapId);
				w.Write(player.X);
				w.Write(player.Y);
				w.Write(player.Z);
				w.Write(player.GuildId);

				uint flag = 0x00000000;
				w.Write(flag);
				w.Write((byte)0);
				w.Write(player.PetDisplayId);
				w.Write(player.PetLevel);
				w.Write(player.PetCreatureFamily);
				w.Write(0);
				for(var i = 0; i < 20; i++) {
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
			client.Send(p);
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCMSG_CHAR_CREATE(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			var player = new Player {
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
			((WorldClient)client).Account.AddPlayer(player);
			var responce = new WorldPacket(WMSG.SMSG_CHAR_CREATE);
			var w = responce.CreateWriter();
			w.Write((byte)47);
			client.Send(responce);
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandleCMSG_PLAYER_LOGIN(ClientBase client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var player = ((WorldClient)client).Account.GetPlayer(guid);
			if(null == player) {
				client.Send(GetCharacterLoginFiledPkt(0x44));
				return;
			}

			client.Send(GetLoginVerifyWorldPkt(player));

			client.Send(GetAccountDataTimesPkt());

			client.Send(GetLoginSetTimeSpeedPkt());

			client.Send(GetUpdateObjectPkt(player));

			client.Send(GetTimeSyncReqPkt());
		}

		private static IPacket GetTimeSyncReqPkt() {
			var r = new WorldPacket(WMSG.SMSG_TIME_SYNC_REQ);
			var w = r.CreateWriter();
			w.Write(0);
			return r;
		}

		private static IPacket GetUpdateObjectPkt(Player player) {
			var r = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
			var w = r.CreateWriter();
			w.Write(player.UpdateObjects());
			return r;
		}

		private static IPacket GetLoginVerifyWorldPkt(Player player) {
			var result = new WorldPacket(WMSG.SMSG_LOGIN_VERIFY_WORLD);
			var w = result.CreateWriter();
			w.Write(player.MapId);
			w.Write(player.X);
			w.Write(player.Y);
			w.Write(player.Z);
			w.Write(player.O);
			return result;
		}

		private static IPacket GetLoginSetTimeSpeedPkt() {
			var result = (IPacket)new WorldPacket(WMSG.SMSG_LOGIN_SETTIMESPEED);
			var w = result.CreateWriter();
			w.Write(Program.GetActualTime());
			w.Write(0.01666667F);
			return result;
		}

		private static IPacket GetAccountDataTimesPkt() {
			var result = (IPacket)new WorldPacket(WMSG.SMSG_ACCOUNT_DATA_TIMES);
			var w = result.CreateWriter();
			w.Write(new byte[0x80]);
			return result;
		}

		private static IPacket GetCharacterLoginFiledPkt(int error) {
			var result = (IPacket)new WorldPacket(WMSG.SMSG_CHARACTER_LOGIN_FAILED);
			var w = result.CreateWriter();
			w.Write((byte)error);
			return result;
		}
	}
}