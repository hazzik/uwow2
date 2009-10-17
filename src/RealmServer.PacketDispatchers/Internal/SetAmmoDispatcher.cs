using System;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_SET_AMMO)]
	internal class SetAmmoDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			client.Player.AmmoId = packet.CreateReader().ReadUInt32();
		}

		#endregion
	}
}