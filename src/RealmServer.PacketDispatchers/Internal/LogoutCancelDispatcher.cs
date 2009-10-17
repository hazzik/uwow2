using System;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_LOGOUT_CANCEL)]
	internal class LogoutCancelDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			if(client.Player == null) {
				return;
			}
			client.Player.StandState = StandStates.Standing;
			client.SendLogoutCancelAck();
		}

		#endregion
	}
}