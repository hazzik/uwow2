using System;
using System.IO;
using System.Linq;
using Hazzik.Creatures;
using Hazzik.Map;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_GOSSIP_SELECT_OPTION)]
	internal class GossipSelectOptionDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong targetGuid = reader.ReadUInt64();
		    uint unk1 = reader.ReadUInt32();
			uint option = reader.ReadUInt32();
		    if(option==1) {
                session.Player.Health = 0;
            }else {
		        session.SendShowBank(targetGuid);
		    }
		}

		#endregion
	}
}