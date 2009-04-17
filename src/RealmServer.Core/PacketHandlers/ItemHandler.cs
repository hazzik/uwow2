using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Objects;
using Hazzik.Repositories;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class ItemHandler {
		[WorldPacketHandler(WMSG.CMSG_ITEM_QUERY_SINGLE)]
		public static void HandleItemQuerySingle(ISession client, IPacket packet) {
			var r = packet.CreateReader();
			var itemId = r.ReadUInt32();
			var template = ItemTemplateRepository.FindById(itemId);
			if(template != null) {
				client.Send(template.GetResponce());
			}
		}

		[WorldPacketHandler(WMSG.CMSG_DESTROYITEM)]
		public static void HandleDestroyItem(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var bag = reader.ReadByte();
			var slot = reader.ReadByte();

			var inventory = client.Player.GetInventory(bag);
			if(inventory != null) {
				inventory.DestroyItem(slot);
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
			
			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.GetInventory(dstBag);
			
			SwapItems(inventorySrc, srcSlot, inventoryDst, dstSlot);
		}

		[WorldPacketHandler(WMSG.CMSG_SWAP_INV_ITEM)]
		public static void HandleSwapInvItem(ISession client,IPacket packet) {
			var reader = packet.CreateReader();
			var srcSlot = reader.ReadByte();
			var dstSlot = reader.ReadByte();
			var player = client.Player;

			SwapItems(player.Inventory, srcSlot, player.Inventory, dstSlot);
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

		[WorldPacketHandler(WMSG.CMSG_SPLIT_ITEM)]
		public static void HandleSplitItem(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var dstBag = reader.ReadByte();
			var dstSlot = reader.ReadByte();
			var amount = reader.ReadByte();

			var player = client.Player;
			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.GetInventory(dstBag);

			var srcItem = inventorySrc[srcSlot];
			var dstItem = inventoryDst[dstSlot];
			if(dstItem == null) {
				dstItem = ItemFactory.Create(srcItem.Template);
				inventoryDst[dstSlot] = dstItem;
			}
			else {
				dstItem.StackCount += amount;
			}
			srcItem.StackCount -= amount;
		}

		[WorldPacketHandler(WMSG.CMSG_AUTOEQUIP_ITEM)]
		public static void HandleAutoEquip(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var srcBag = reader.ReadByte();
			var srcSlot = reader.ReadByte();
			var player = client.Player;

			var inventorySrc = player.GetInventory(srcBag);
			var inventoryDst = player.Inventory;

			var item = inventorySrc[srcSlot];
			var dstSlot = (int)item.Template.CanBeEquipedIn[0];
			inventorySrc[srcSlot] = inventoryDst[dstSlot];
			inventoryDst[dstSlot] = item;
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

			var dstSlot = inventoryDst.FindFreeSlot();

			var item = inventorySrc[srcSlot];
			inventorySrc[srcSlot] = inventoryDst[dstSlot];
			inventoryDst[dstSlot] = item;
		}

		[WorldPacketHandler(WMSG.CMSG_SET_AMMO)]
		public static void HandleSetAmmo(ISession client, IPacket packet) {
			client.Player.AmmoId = packet.CreateReader().ReadUInt32();
		}
	}
}
