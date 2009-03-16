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
		public static void HandleCMSG_NAME_QUERY(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();

			var player = ObjectManager.GetPlayersNear(client.Player).FirstOrDefault(x => x.Guid == guid);

			if(player != null) {
				client.Send(player.GetNameQueryResponcePkt());
			}
		}

		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleCMSG_REALM_SPLIT(ISession client, IPacket packet) {
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
		public static void HandleCMSG_CHAR_ENUM(ISession client, IPacket packet) {
			client.Send(client.Account.GetCharEnumPkt());
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCMSG_CHAR_CREATE(ISession client, IPacket packet) {
			var account = client.Account;
			var r = packet.CreateReader();
			var player = new Player {
				Name = r.ReadCString(),
				Race = (Races)r.ReadByte(),
				Classe = (Classes)r.ReadByte(),
				Gender = (GenderType)r.ReadByte(),
				Skin = r.ReadByte(),
				Face = r.ReadByte(),
				HairStyle = r.ReadByte(),
				HairColor = r.ReadByte(),
				FacialHair = r.ReadByte(),
			};
			account.AddPlayer(player);
			client.Send(Account.GetCharCreatePkt(47));
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandleCMSG_PLAYER_LOGIN(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var player = client.Account.GetPlayer(guid);
			if(null == player) {
				client.Send(Account.GetCharacterLoginFiledPkt(0x44));
				return;
			}
			
			client.Player = player;
			ObjectManager.Add(player);

			client.Send(player.GetLoginVerifyWorldPkt());

			client.Send(Account.GetAccountDataTimesPkt());

			client.Send(GetLoginSetTimeSpeedPkt());

			player.UpdateObjects();

			client.Send(GetTimeSyncReqPkt());

			player.StartUpdateTimer();
		}

		private static IPacket GetTimeSyncReqPkt() {
			return new WorldPacket(WMSG.SMSG_TIME_SYNC_REQ, new byte[4]);
		}

		private static IPacket GetLoginSetTimeSpeedPkt() {
			var result = (IPacket)new WorldPacket(WMSG.SMSG_LOGIN_SETTIMESPEED);
			var w = result.CreateWriter();
			w.Write(Program.GetActualTime());
			w.Write(0.01666667F);
			return result;
		}

		[WorldPacketHandler(WMSG.CMSG_SETSHEATHED)]
		public static void HandleCMSG_SETSHEATHED(ISession client, IPacket packet) {
			client.Player.Sheath = (SheathType)packet.CreateReader().ReadInt32();
		}

		[WorldPacketHandler(WMSG.CMSG_STANDSTATECHANGE)]
		public static void HandleCMSG_STANDSTATECHANGE(ISession client, IPacket packet) {
			client.Player.StandState = (StandStates)packet.CreateReader().ReadByte();
		}

		[WorldPacketHandler(WMSG.CMSG_LOGOUT_REQUEST)]
		public static void HandleCMSG_LOGOUT_REQUEST(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Sitting;
			client.Send(GetLogoutResponcePkt(LogoutResponses.Accepted));
		}

		[WorldPacketHandler(WMSG.CMSG_LOGOUT_CANCEL)]
		public static void Handle(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Standing;
			client.Send(GetLogoutCancelAckPkt());
		}

		private static IPacket GetLogoutCancelAckPkt() {
			return new WorldPacket(WMSG.SMSG_LOGOUT_CANCEL_ACK);
		}

		private static IPacket GetLogoutResponcePkt(LogoutResponses error) {
			var result = new WorldPacket(WMSG.SMSG_LOGOUT_RESPONSE);
			var writer = result.CreateWriter();
			writer.Write((byte)error);
			writer.Write(0);
			return result;
		}
	}
}