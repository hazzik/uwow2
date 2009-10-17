using System;
using System.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_SWAP_ITEM)]
	internal class SwapItemDispatcher : SwapItemDispatcherBase, IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte dstBag = reader.ReadByte();
			byte dstSlot = reader.ReadByte();
			byte srcBag = reader.ReadByte();
			byte srcSlot = reader.ReadByte();

			Player player = session.Player;

			SwapItems(player.GetInventory(srcBag), srcSlot, player.GetInventory(dstBag), dstSlot);
		}

		#endregion
	}
}