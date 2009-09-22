using System;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_BANKER_ACTIVATE)]
	public class BankerActivateDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.SendShowBank(packet.CreateReader().ReadUInt64());
		}

		#endregion
	}
}