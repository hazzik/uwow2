using System;
using System.IO;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_AUTOSTORE_BAG_ITEM)]
	internal class AutoStoreBagItemDispatcher:IPacketDispatcher {
		public void Dispatch(ISession client, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();
			byte dstBag = reader.ReadByte();

			Player player = client.Player;

			IInventory inventorySrc = player.GetInventory(srcBag);
			IInventory inventoryDst = player.GetInventory(dstBag);

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}
	}
}