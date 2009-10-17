using System;
using System.IO;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_SPLIT_ITEM)]
	internal class SplitItemDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();
			byte dstBag = reader.ReadByte();
			byte dstSlot = reader.ReadByte();
			byte amount = reader.ReadByte();

			Player player = client.Player;

			SplitItems(player.GetInventory(srcBag), srcSlot, player.GetInventory(dstBag), dstSlot, amount);
		}

		#endregion

		private static void SplitItems(IInventory inventorySrc, int srcSlot, IInventory inventoryDst, int dstSlot, int amount) {
			Item srcItem = inventorySrc[srcSlot];
			Item dstItem = inventoryDst[dstSlot];
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
	}
}