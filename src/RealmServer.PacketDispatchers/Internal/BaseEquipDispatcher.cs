using System;
using System.IO;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	internal abstract class BaseEquipDispatcher {
		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();

			Player player = session.Player;

			IInventory inventorySrc = player.GetInventory(srcBag);
			IInventory inventoryDst = GetInventoryDst(player);

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		protected abstract IInventory GetInventoryDst(Player player);
	}
}