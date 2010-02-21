using System;
using Hazzik.Gossip;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
    [WorldPacketHandler(WMSG.CMSG_GOSSIP_HELLO)]
    internal class GossipHelloDispatcher : IPacketDispatcher {
        #region IPacketDispatcher Members

        public void Dispatch(ISession session, IPacket packet) {
            ulong targetGuid = packet.CreateReader().ReadUInt64();
        
            var message = new GossipMessage(2, new[] {
                new GossipMenuItem(2, GossipMenuIcon.Banker, false, "Need bank?"),
                new GossipMenuItem(1, GossipMenuIcon.Gossip, false, "I would die!")
            }, new[] {
                new QuestsMenuItem(3, 2, "hi"),
            });

            session.SendGossipMessage(targetGuid, message);
        }

        #endregion
    }
}