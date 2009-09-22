using System;
using System.Collections.Generic;
using Hazzik.Attributes;
using Hazzik.Gossip;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_GOSSIP_HELLO)]
	internal class GossipHelloDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			ulong targetGuid = packet.CreateReader().ReadUInt64();
			var message = new GossipMessage(2, new List<GossipMenuItem> {
				new GossipMenuItem(2, GossipMenuIcon.Banker, false, "Hello?"),
			}, null);
			session.SendGossipMessage(targetGuid, message);
		}

		#endregion
	}
}