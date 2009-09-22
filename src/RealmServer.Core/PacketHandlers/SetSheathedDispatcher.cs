using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_SETSHEATHED)]
	internal class SetSheathedDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.Sheath = (SheathType)packet.CreateReader().ReadInt32();
		}

		#endregion
	}
}