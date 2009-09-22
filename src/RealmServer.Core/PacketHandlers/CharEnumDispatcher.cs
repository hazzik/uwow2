using System;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_CHAR_ENUM)]
	public class CharEnumDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.SendCharEnum();
		}

		#endregion
	}
}