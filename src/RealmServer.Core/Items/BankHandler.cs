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

		[WorldPacketHandler(WMSG.CMSG_BUY_BANK_SLOT)]
		public static void HandleBuyBankSlot(ISession session, IPacket packet) {
			var guid = packet.CreateReader().ReadUInt64();
			session.Player.BankBags.BuySlot();
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOSTORE_BANK_ITEM)]
		public static void HandleAutoStoreBankItem(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var player = session.Player;
			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.Inventory;

			inventoryDst.AutoAdd(inventorySrc[srcSlot]);
			inventorySrc[srcSlot] = null;
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOBANK_ITEM)]
		public static void HandleAutoBankItem(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var player = session.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.Bank;

			inventoryDst.AutoAdd(inventorySrc[srcSlot]);
			inventorySrc[srcSlot] = null;
		}
	}
}