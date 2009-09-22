using System;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_SET_AMMO)]
	internal class SetAmmoDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.AmmoId = packet.CreateReader().ReadUInt32();
		}

		#endregion
	}
}