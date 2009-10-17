using System;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_AUTOSTORE_BANK_ITEM)]
	internal class AutoStoreBankItemDispatcher : BaseEquipDispatcher, IPacketDispatcher {
		protected override IInventory GetInventoryDst(Player player) {
			return player.BackPack;
		}
	}
}