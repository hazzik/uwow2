using System;
using System.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_SWAP_INV_ITEM)]
	internal class SwapInvItemDispatcher : SwapItemDispatcherBase, IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte srcSlot = reader.ReadByte();
			byte dstSlot = reader.ReadByte();

			Player player = session.Player;

			SwapItems(player.Inventory, srcSlot, player.Inventory, dstSlot);
		}

		#endregion
	}
}