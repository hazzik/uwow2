using System;
using System.IO;
using Hazzik.Creatures;
using Hazzik.Data;
using Hazzik.Data.Fake.Templates;
using Hazzik.GameObjects;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
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
			//Creature creature = Creature.Create(Repository.CreatureTemplate.FindById(647));
			//creature.NpcFlags = NpcFlags.Banker;
			//creature.BaseHealth = 500;
			//creature.Health = 500;
			//creature.PosX = player.PosX;
			//creature.PosY = player.PosY;
			//creature.PosZ = player.PosZ;
			//ObjectManager.Add(creature);

			GameObject gameObject = GameObject.Create(Repository.GameObjectTemplate.FindById(1731));
			gameObject.PosX = player.PosX;
			gameObject.PosY = player.PosY;
			gameObject.PosZ = player.PosZ;
			ObjectManager.Add(gameObject);

			var corpse =Corpse.Create(player);
			corpse.PosX = player.PosX;
			corpse.PosY = player.PosY;
			corpse.PosZ = player.PosZ;
			corpse.Flags |= CorpseFlags.IsClaimed;
			corpse.DynamicFlags |= CorpseDynamicFlags.PlayerLootable;
			ObjectManager.Add(corpse);

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