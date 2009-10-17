using System;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_LOGOUT_REQUEST)]
	internal class LogoutRequestDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Sitting;
			client.SendLogoutResponce();
			client.LogOut();
			client.SendLogoutComplete();
		}

		#endregion
	}
}