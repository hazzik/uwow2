using System;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.Items {
	[PacketHandlerClass]
	public class BankHandler {
		[WorldPacketHandler(WMSG.CMSG_BANKER_ACTIVATE)]
		public static void HandleBankerActivate(ISession session, IPacket packet) {
			session.SendShowBank(packet.CreateReader().ReadUInt64());
		}
	}
}