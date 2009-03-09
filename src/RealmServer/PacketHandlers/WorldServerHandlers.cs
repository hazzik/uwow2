using System;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik {
	[PacketHandlerClass]
	public static class WorldServerHandlers {
		[WorldPacketHandler(WMSG.CMSG_NAME_QUERY)]
		public static void HandleCMSG_NAME_QUERY(ClientBase client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();

			var player = ObjectManager.GetPlayersNear(((WorldClient)client).Player).FirstOrDefault(x => x.Guid == guid);

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

			wclient.Send(player.GetLoginVerifyWorldPkt());

			wclient.Send(RealmAccount.GetAccountDataTimesPkt());

			wclient.Send(GetLoginSetTimeSpeedPkt());

			wclient.Send(player.GetUpdateObjectPkt());

			wclient.Send(GetTimeSyncReqPkt());

			wclient.Player = player;
			ObjectManager.Add(player);
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

		[WorldPacketHandler(WMSG.CMSG_STANDSTATECHANGE)]
		public static void HandleCMSG_STANDSTATECHANGE(ClientBase client, IPacket packet) {
			var r = packet.CreateReader();
			((WorldClient)client).Player.StandState = (StandStates)r.ReadByte();
		}

		[WorldPacketHandler(WMSG.CMSG_LOGOUT_REQUEST)]
		private static void __handleCMSG_LOGOUT_REQUEST(ClientBase client, IPacket packet) {
			client.Send(GetLogoutResponse());
		}

		private static WorldPacket GetLogoutResponse() {
			var response = new WorldPacket(WMSG.SMSG_LOGOUT_RESPONSE);
			var writer = response.CreateWriter();
			writer.Write((uint)0);
			writer.Write((byte)0);
			return response;
		}
	}
}