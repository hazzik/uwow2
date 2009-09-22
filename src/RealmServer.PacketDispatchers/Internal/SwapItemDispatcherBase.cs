using System;
using Hazzik.Items;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	internal class SwapItemDispatcherBase {
		protected void SwapItems(IInventory srcInventory, int srcSlot, IInventory dstInventory, int dstSlot) {
			Item srcItem = srcInventory[srcSlot];
			Item dstItem = dstInventory[dstSlot];

			if(dstItem != null && dstItem.CanStack(srcItem)) {
				uint totalCount = srcItem.StackCount + dstItem.StackCount;
				int maxAmount = dstItem.Template.MaxAmount;
				if(totalCount <= maxAmount) {
					dstItem.StackCount = totalCount;
					srcItem.Destroy();
					srcInventory[srcSlot] = null;
				}
				else {
					dstItem.StackCount = (uint)maxAmount;
					srcItem.StackCount = (uint)(totalCount - maxAmount);
				}
			}
			else {
				srcInventory[srcSlot] = dstItem;
				dstInventory[dstSlot] = srcItem;
			}
		}
	}
}