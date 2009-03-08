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
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt32();

			var player = ((WorldClient)client).Account.GetPlayer(guid);
			if(player != null) {
				client.Send(player.GetNameQueryResponcePkt());
			}
		}

		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleCMSG_REALM_SPLIT(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			var unk1 = r.ReadUInt32();

			client.Send(GetRealmSplitPkt(unk1));
		}

		private static IPacket GetRealmSplitPkt(uint unk1) {
			var responce = new WorldPacket(WMSG.SMSG_REALM_SPLIT);
			var w = responce.CreateWriter();
			w.Write(unk1);
			//0-normal, 1-split, 2-split pending;
			w.Write(0);
			w.WriteCString(DateTime.Now.AddDays(1).ToShortDateString());
			return responce;
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
		public static void HandleCMSG_CHAR_ENUM(ClientBase client, IPacket packet) {
			client.Send(((WorldClient)client).Account.GetCharEnumPkt());
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCMSG_CHAR_CREATE(ClientBase client, IPacket packet) {
			var account = ((WorldClient)client).Account;
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
			account.AddPlayer(player);
			client.Send(RealmAccount.GetCharCreatePkt(47));
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandleCMSG_PLAYER_LOGIN(ClientBase client, IPacket packet) {
			var wclient = (WorldClient)client;

			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var player = wclient.Account.GetPlayer(guid);
			if(null == player) {
				client.Send(RealmAccount.GetCharacterLoginFiledPkt(0x44));
				return;
			}

			client.Send(player.GetLoginVerifyWorldPkt());

			client.Send(RealmAccount.GetAccountDataTimesPkt());

			client.Send(GetLoginSetTimeSpeedPkt());

			client.Send(player.GetUpdateObjectPkt());

			client.Send(GetTimeSyncReqPkt());

			wclient.Player = player;
			Program.AllConnected.Add(player);
			wclient.StartUpdateTimer();
		}

		private static IPacket GetTimeSyncReqPkt() {
			var r = new WorldPacket(WMSG.SMSG_TIME_SYNC_REQ);
			var w = r.CreateWriter();
			w.Write(0);
			return r;
		}

		private static IPacket GetLoginSetTimeSpeedPkt() {
			var result = (IPacket)new WorldPacket(WMSG.SMSG_LOGIN_SETTIMESPEED);
			var w = result.CreateWriter();
			w.Write(Program.GetActualTime());
			w.Write(0.01666667F);
			return result;
		}

		[WorldPacketHandler(WMSG.CMSG_SETSHEATHED)]
		public static void HandleCMSG_SETSHEATHED(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			((WorldClient)client).Player.Sheath = (SheathType)r.ReadInt32();
		}
	}
}