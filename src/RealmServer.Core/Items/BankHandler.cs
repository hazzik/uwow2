using System;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.Items {
	[PacketHandlerClass]
	public class BankHandler {
		[WorldPacketHandler(WMSG.CMSG_BANKER_ACTIVATE)]
		public static void HandleBankerActivate(ISession session, IPacket packet) {
			var guid = packet.CreateReader().ReadUInt64();
			BankerActivate(session, guid);
		}

		private static void BankerActivate(ISession session, ulong guid) {
			ShowBank(session, guid);
		}

		private static void ShowBank(ISession session, ulong guid) {
			session.Client.Send(GetShowBankPkt(guid));
		}

		private static IPacket GetShowBankPkt(ulong guid) {
			var responce = WorldPacketFactory.Create(WMSG.SMSG_SHOW_BANK);
			var writer = responce.CreateWriter();
			writer.Write(guid);
			return responce;
		}
	}
}