using System;
using System.IO;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_AUTOBANK_ITEM)]
	internal class AutoBankItemDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();

			Player player = session.Player;

			IInventory inventorySrc = player.GetInventory(srcBag);
			IInventory inventoryDst = player.Bank;

			if(inventoryDst.AutoAdd(inventorySrc[srcSlot])) {
				inventorySrc[srcSlot] = null;
			}
		}

		#endregion
	}
}