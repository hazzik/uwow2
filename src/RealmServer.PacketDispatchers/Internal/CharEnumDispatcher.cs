using System;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_CHAR_ENUM)]
	internal class CharEnumDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.SendCharEnum();
		}

		#endregion
	}
}