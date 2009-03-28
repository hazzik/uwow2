using System;

namespace Hazzik.Objects {
	public partial class Item {
		#region ITEM_FIELD_CREATOR

		//ITEM_FIELD_CREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 Creator { get; set; }

		#endregion

		#region ITEM_FIELD_GIFTCREATOR

		//ITEM_FIELD_GIFTCREATOR : type = Long, size = 2, flag = Public
		public virtual UInt64 GiftCreator { get; set; }

		#endregion

		#region ITEM_FIELD_STACK_COUNT

		//ITEM_FIELD_STACK_COUNT : type = Int, size = 1, flag = Owner, ItemOwner
		public virtual UInt32 StackCount { get; set; }

		#endregion

		#region ITEM_FIELD_DURATION

		//ITEM_FIELD_DURATION : type = Int, size = 1, flag = Owner, ItemOwner
		public virtual UInt32 Duration { get; set; }

		#endregion

		#region ITEM_FIELD_SPELL_CHARGES

		//ITEM_FIELD_SPELL_CHARGES : type = Int, size = 5, flag = Owner, ItemOwner

		#endregion

		#region ITEM_FIELD_FLAGS

		//ITEM_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags { get; set; }

		#endregion

		#region ITEM_FIELD_ENCHANTMENT_1_1 

		//ITEM_FIELD_ENCHANTMENT_1_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_1_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_2_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_2_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_3_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_3_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_4_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_4_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_5_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_5_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_6_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_6_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_7_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_7_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_8_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_8_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_9_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_9_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_10_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_10_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_11_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_11_3 : type = Shorts, size = 1, flag = Public

		//ITEM_FIELD_ENCHANTMENT_12_1 : type = Int, size = 2, flag = Public
		//ITEM_FIELD_ENCHANTMENT_12_3 : type = Shorts, size = 1, flag = Public

		#endregion

		#region ITEM_FIELD_PROPERTY_SEED

		//ITEM_FIELD_PROPERTY_SEED : type = Int, size = 1, flag = Public
		public virtual UInt32 PropertySeed { get; set; }

		#endregion

		#region ITEM_FIELD_RANDOM_PROPERTIES_ID

		//ITEM_FIELD_RANDOM_PROPERTIES_ID : type = Int, size = 1, flag = Public
		public virtual UInt32 RandomPropertiesId { get; set; }

		#endregion

		#region ITEM_FIELD_ITEM_TEXT_ID

		//ITEM_FIELD_ITEM_TEXT_ID : type = Int, size = 1, flag = Owner
		public virtual UInt32 ItemTextId { get; set; }

		#endregion

		#region ITEM_FIELD_DURABILITY

		//ITEM_FIELD_DURABILITY : type = Int, size = 1, flag = Owner, ItemOwner
		public virtual UInt32 Durability { get; set; }

		#endregion

		#region ITEM_FIELD_MAXDURABILITY

		//ITEM_FIELD_MAXDURABILITY : type = Int, size = 1, flag = Owner, ItemOwner
		public virtual UInt32 MaxDurability { get; set; }

		#endregion

		//ITEM_FIELD_PAD : type = Int, size = 1, flag = None
	}
}