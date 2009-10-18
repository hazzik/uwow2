using System;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_RECLAIM_CORPSE)]
	internal class ReclaimCorpse : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.Player.Corpse.Flags |= CorpseFlags.IsClaimed;
			session.Player.Ghost = false;
			session.Player.Health = 100;
		}

		#endregion
	}
}