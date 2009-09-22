using System;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_BANKER_ACTIVATE)]
	internal class BankerActivateDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.SendShowBank(packet.CreateReader().ReadUInt64());
		}

		#endregion
	}
}