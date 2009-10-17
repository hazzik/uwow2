using System;
using Hazzik.Items;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_AUTOBANK_ITEM)]
	internal class AutoBankItemDispatcher : BaseEquipDispatcher, IPacketDispatcher {
		protected override IInventory GetInventoryDst(Player player) {
			return player.Bank;
		}
	}
}