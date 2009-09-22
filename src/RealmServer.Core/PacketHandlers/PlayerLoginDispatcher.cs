using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_PLAYER_LOGIN)]
	internal class PlayerLoginDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
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

		#endregion
	}
}