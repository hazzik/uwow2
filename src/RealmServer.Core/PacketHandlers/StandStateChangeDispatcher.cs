using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_STANDSTATECHANGE)]
	internal class StandStateChangeDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.StandState = (StandStates)packet.CreateReader().ReadByte();
		}

		#endregion
	}
}