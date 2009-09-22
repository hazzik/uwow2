using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Items;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_DESTROYITEM)]
	internal class DestroyItemDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			byte bag = reader.ReadByte();
			byte slot = reader.ReadByte();

			IInventory inventory = session.Player.GetInventory(bag);
			if(inventory != null) {
				inventory.DestroyItem(slot);
			}
		}

		#endregion
	}
}