using System;
using Hazzik.Creatures;
using Hazzik.GameObjects;
using Hazzik.Gossip;
using Hazzik.Items;
using Hazzik.Objects;
using Hazzik.Objects.Update;

namespace Hazzik.Net {
	public interface ISession {
		Account Account { get; set; }
		Player Player { get; }
	    void SendHeartBeat();
		void SendNameQueryResponce(Player player);
		void SendRealmSplitPkt(uint unk1);
		void SendCharEnum();
		void SendCharCreate();
		void SendAccountDataTimes(uint mask);
		void SendLogoutComplete();
		void SendLogoutResponce();
		void SendLogoutCancelAck();
		void SendShowBank(ulong guid);
		void SendDestroy(WorldObject item);
		void SendCreatureQueryResponce(CreatureTemplate creature);
		void SendGameObjectQueryResponce(GameObjectTemplate template);
		void SendItemQuerySingleResponse(ItemTemplate template);
		void SendNpcTextUpdate(NpcTexts text);
		void SendStandstateUpdate();
		void SendGossipMessage(ulong targetGuid, GossipMessage message);
		void Send(IPacketBuilder packetBuilder);
		void LogOut();
		void Login(Player player);
	    void Send(IPacket packet);
	}
}