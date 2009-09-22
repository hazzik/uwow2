using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_LOGOUT_CANCEL)]
	internal class LogoutCancelDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.StandState = StandStates.Standing;
			client.SendLogoutCancelAck();
		}

		#endregion
	}
}