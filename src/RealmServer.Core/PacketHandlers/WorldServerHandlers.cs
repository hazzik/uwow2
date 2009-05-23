using System;
using System.IO;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik {
	[PacketHandlerClass]
	public static class WorldServerHandlers {
		[WorldPacketHandler(WMSG.CMSG_NAME_QUERY)]
		public static void HandleNameQuery(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();

			Player player = ObjectManager.GetPlayersNear(session.Player).FirstOrDefault(x => x.Guid == guid);

			if(player != null) {
				session.SendNameQueryResponce(player);
			}
		}

		[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
		public static void HandleRealmSplit(ISession session, IPacket packet) {
			BinaryReader r = packet.CreateReader();
			uint unk1 = r.ReadUInt32();

			session.SendRealmSplitPkt(unk1);
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
		public static void HandleCharEnum(ISession session, IPacket packet) {
			session.SendCharEnum();
		}

		[WorldPacketHandler(WMSG.CMSG_CHAR_CREATE)]
		public static void HandleCharCreate(ISession session, IPacket packet) {
			Account account = session.Account;
			BinaryReader r = packet.CreateReader();
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
			new FakeCharacterCreateHandler(player).Init();
			account.AddPlayer(player);
			//Repository.Account.Save(account);
			//Repository.Account.SubmitChanges();
			session.SendCharCreate();
		}

		[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
		public static void HandlePlayerLogin(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();
			Player player = session.Account.GetPlayer(guid);
			if(null == player) {
				session.SendCharacterLoginFiled();
				return;
			}

			session.Player = player;
			ObjectManager.Add(player);
			//ObjectManager.Add(new Creature(new Creature647()) {
			//   NpcFlags = /*NpcFlags.Gossip | NpcFlags.QuestGiver |*/ NpcFlags.Banker,
			//   BaseHealth = 500,
			//   Health = 500,
			//   PosX = player.PosX,
			//   PosY = player.PosY,
			//   PosZ = player.PosZ,
			//});
			GameObject gameObject = GameObject.Create(Repository.GameObjectTemplate.FindById(2489));
			gameObject.PosX = player.PosX;
			gameObject.PosY = player.PosY;
			gameObject.PosZ = player.PosZ;
			ObjectManager.Add(gameObject);
			session.SendLoginVerifyWorld();

			session.SendAccountDataTimes();

			session.SendLoginSetTimeSpeed();
			session.SendSetProficiency(2, -1);
			session.SendSetProficiency(4, -1);
			session.SendSetProficiency(6, -1);
			session.SendInitialSpells();
			var manager = new UpdateManager(player);
			manager.UpdateObjects();

			session.SendTimeSyncReq();

			manager.StartUpdateTimer();
		}

		[WorldPacketHandler(WMSG.CMSG_SETSHEATHED)]
		public static void HandleSetSheathed(ISession client, IPacket packet) {
			client.Player.Sheath = (SheathType)packet.CreateReader().ReadInt32();
		}

		[WorldPacketHandler(WMSG.CMSG_STANDSTATECHANGE)]
		public static void HandleStandStateChange(ISession client, IPacket packet) {
			client.Player.StandState = (StandStates)packet.CreateReader().ReadByte();
		}

		[WorldPacketHandler(WMSG.CMSG_LOGOUT_REQUEST)]
		public static void HandleLogoutRequest(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Sitting;
			client.SendLogoutResponce();
			client.Player = null;
			client.SendLogoutComplete();
		}

		[WorldPacketHandler(WMSG.CMSG_LOGOUT_CANCEL)]
		public static void HandleLogoutCancel(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Standing;
			client.SendLogoutCancelAck();
		}

		public static uint GetActualTime() {
			DateTime time = DateTime.Now;
			int year = time.Year - 2000;
			int month = time.Month - 1;
			int day = time.Day - 1;
			var dayOfWeek = (int)time.DayOfWeek;
			int hour = time.Hour;
			int minute = time.Minute;

			return (uint)(minute | (hour << 0x06) | (dayOfWeek << 0x0B) | (day << 0x0E) | (month << 0x14) | (year << 0x18));
		}
	}
}