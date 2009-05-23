using System;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.Items {
	[PacketHandlerClass]
	public class ItemHandler {
		[WorldPacketHandler(WMSG.CMSG_ITEM_QUERY_SINGLE)]
		public static void HandleItemQuerySingle(ISession session, IPacket packet) {
			var r = packet.CreateReader();
			var itemId = r.ReadUInt32();
			var template = Repository.ItemTemplate.FindById(itemId);
			if(template != null) {
				session.Client.Send(template.GetResponce());
			}
		}

		[WorldPacketHandler(WMSG.CMSG_DESTROYITEM)]
		public static void HandleDestroyItem(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var bag = reader.ReadByte();
			var slot = reader.ReadByte();

			var inventory = session.Player.GetInventory(bag);
			if(inventory != null) {
				inventory.DestroyItem(slot);
			}
		}
		
		private static void SwapItems(IInventory srcInventory, int srcSlot, IInventory dstInventory, int dstSlot) {
			var srcItem = srcInventory[srcSlot];
			var dstItem = dstInventory[dstSlot];

			if(dstItem != null && dstItem.CanStack(srcItem)) {
				var totalCount = srcItem.StackCount + dstItem.StackCount;
				var maxAmount = dstItem.Template.MaxAmount;
				if(totalCount <= maxAmount) {
					dstItem.StackCount = totalCount;
					srcItem.Destroy();
					srcInventory[srcSlot] = null;
				}
				else {
					dstItem.StackCount = (uint)maxAmount;
					srcItem.StackCount = (uint)(totalCount - maxAmount);
				}
			}
			else {
				srcInventory[srcSlot] = dstItem;
				dstInventory[dstSlot] = srcItem;
			}
		}

		private static void SplitItems(IInventory inventorySrc, int srcSlot, IInventory inventoryDst, int dstSlot, int amount) {
			var srcItem = inventorySrc[srcSlot];
			var dstItem = inventoryDst[dstSlot];
			if(dstItem == null) {
				dstItem = Item.Create(srcItem.Template);
				inventoryDst[dstSlot] = dstItem;
				dstItem.StackCount = (byte)amount;
				srcItem.StackCount -= (byte)amount;
			}
			else if(dstItem.CanStack(srcItem)) {
				dstItem.StackCount += (byte)amount;
				srcItem.StackCount -= (byte)amount;
			}
		}

		[WorldPacketHandler(WMSG.CMSG_SWAP_ITEM)]
		public static void HandleSwapItem(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var dstBag = reader.ReadByte();
			var dstSlot = reader.ReadByte();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();

			var player = client.Player;

			SwapItems(player.GetInventory(srcBag), srcSlot, player.GetInventory(dstBag), dstSlot);
		}

		[WorldPacketHandler(WMSG.CMSG_SWAP_INV_ITEM)]
		public static void HandleSwapInvItem(ISession client,IPacket packet) {
			var reader = packet.CreateReader();
			var srcSlot = reader.ReadByte();
			var dstSlot = reader.ReadByte();
			var player = client.Player;

			SwapItems(player.Inventory, srcSlot, player.Inventory, dstSlot);
		}


		[WorldPacketHandler(WMSG.CMSG_SPLIT_ITEM)]
		public static void HandleSplitItem(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var dstBag = reader.ReadByte();
			var dstSlot = reader.ReadByte();
			var amount = reader.ReadByte();

			var player = client.Player;

			SplitItems(player.GetInventory(srcBag), srcSlot, player.GetInventory(dstBag), dstSlot, amount);
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOSTORE_BAG_ITEM)]
		public static void HandleAutoStoreBagItem(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var dstBag = reader.ReadByte();

			var player = client.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.GetInventory(dstBag);

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOEQUIP_ITEM)]
		public static void HandleAutoEquip(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();

			var player = client.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.Equipment;

			inventorySrc[srcSlot] = inventoryDst.AutoEquip(inventorySrc[srcSlot]);
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOSTORE_BANK_ITEM)]
		public static void HandleAutoStoreBankItem(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();

			var player = session.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.BackPack;

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOBANK_ITEM)]
		public static void HandleAutoBankItem(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();

			var player = session.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.Bank;

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		[WorldPacketHandler(WMSG.CMSG_BUY_BANK_SLOT)]
		public static void HandleBuyBankSlot(ISession session, IPacket packet) {
			var guid = packet.CreateReader().ReadUInt64();
			session.Player.BankBags.BuySlot();
		}

		[WorldPacketHandler(WMSG.CMSG_SET_AMMO)]
		public static void HandleSetAmmo(ISession client, IPacket packet) {
			client.Player.AmmoId = packet.CreateReader().ReadUInt32();
		}
	}
}