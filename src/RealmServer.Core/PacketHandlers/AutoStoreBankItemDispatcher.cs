using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_AUTOSTORE_BANK_ITEM)]
	internal class AutoStoreBankItemDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();

			Player player = session.Player;

			IInventory inventorySrc = player.GetInventory(srcBag);
			IInventory inventoryDst = player.BackPack;

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		#endregion
	}
}