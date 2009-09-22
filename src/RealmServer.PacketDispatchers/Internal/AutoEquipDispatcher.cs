using System;
using System.IO;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_AUTOEQUIP_ITEM)]
	internal class AutoEquipDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();

			Player player = client.Player;

			IInventory inventorySrc = player.GetInventory(srcBag);
			IEquipmentInventory inventoryDst = player.Equipment;

			inventorySrc[srcSlot] = inventoryDst.AutoEquip(inventorySrc[srcSlot]);
		}

		#endregion
	}
}