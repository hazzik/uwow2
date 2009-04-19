using System;
using Hazzik.Objects.Update;

namespace Hazzik.Items {
	public enum InventorySlot {
		None = -1,
		Head = 0,
		Neck = 1,
		Shoulders = 2,
		Shirt = 3,
		Chest = 4,
		Waist = 5,
		Legs = 6,
		Feet = 7,
		Wrists = 8,
		Hands = 9,
		FingerLeft = 10,
		FingerRight = 11,
		TrinketLeft = 12,
		TrinketRight = 13,
		Back = 14,
		MainHand = 15,
		OffHand = 16,
		Ranged = 17,
		Tabard = 18,
		Bag1 = 19,
		Bag2 = 20,
		Bag3 = 21,
		BagLast = 22,
		BackpackStart,
		BackpackEnd = BackpackStart + UpdateFields.PLAYER_FIELD_BANK_SLOT_1 - UpdateFields.PLAYER_FIELD_PACK_SLOT_1,
	}
}