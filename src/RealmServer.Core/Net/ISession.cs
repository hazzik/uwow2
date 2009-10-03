using System;
using System.Security.Cryptography;
using Hazzik.Creatures;
using Hazzik.GameObjects;
using Hazzik.Gossip;
using Hazzik.Items;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik.Net {
	public interface ISession {
		Account Account { get; set; }
		Player Player { get; set; }
		IPacketSender Client { get; }
		void SendHeartBeat();
		void SendInitialSpells();
		void SendNameQueryResponce(Player player);
		void SendRealmSplitPkt(uint unk1);
		void SendCharEnum();
		void SendCharCreate();
		void SendCharacterLoginFiled();
		void SendLoginVerifyWorld();
		void SendAccountDataTimes(uint mask);
		void SendLoginSetTimeSpeed();
		void SendTimeSyncReq();
		void SendLogoutComplete();
		void SendLogoutResponce();
		void SendLogoutCancelAck();
		void SendShowBank(ulong guid);
		void SendDestroy(WorldObject item);
		void SendCreatureQueryResponce(CreatureTemplate creature);
		void SendGameObjectQueryResponce(GameObjectTemplate template);
		void SendItemQuerySingleResponse(ItemTemplate template);
		void SendSetProficiency(byte type, int bitmask);
		void SendNpcTextUpdate(NpcTexts text);
		void SendStandstateUpdate();
		void SendGossipMessage(ulong targetGuid, GossipMessage message);
		void SendUpdateObjects(IPacketBuilder builder);
	}
}