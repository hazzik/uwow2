using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_REALM_SPLIT)]
	internal class RealmSplitDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader r = packet.CreateReader();
			uint unk1 = r.ReadUInt32();

			session.SendRealmSplitPkt(unk1);
		}

		#endregion
	}
}