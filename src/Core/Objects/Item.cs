using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public class Item : WorldObject {
		public Item()
			: base((int)UpdateFields.ITEM_END) {

		}

		protected Item(int updateMaskLength)
			: base(updateMaskLength) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Item; }
		}

		#region UpdateFields
		//ITEM_FIELD_OWNER = OBJECT_END + 0, // 2 4 1
		public long OwnerGuid { get; set; }
		//ITEM_FIELD_CONTAINED = OBJECT_END + 2, // 2 4 1
		public long ContainedGuid { get; set; }
		//ITEM_FIELD_CREATOR = OBJECT_END + 4, // 2 4 1
		public long CreatorGuid { get; set; }
		//ITEM_FIELD_GIFTCREATOR = OBJECT_END + 6, // 2 4 1
		public long GiftCreatorGuid { get; set; }
		//ITEM_FIELD_STACK_COUNT = OBJECT_END + 8, // 1 1 20
		public int StackCount { get; set; }
		//ITEM_FIELD_DURATION = OBJECT_END + 9, // 1 1 20
		public int Duration { get; set; }
		//ITEM_FIELD_SPELL_CHARGES = OBJECT_END + 10, // 5 1 20
		//ITEM_FIELD_FLAGS = OBJECT_END + 15, // 1 1 1
		public int Flags { get; set; }
		//ITEM_FIELD_ENCHANTMENT_1_1 = OBJECT_END + 16, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_1_3 = OBJECT_END + 18, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_2_1 = OBJECT_END + 19, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_2_3 = OBJECT_END + 21, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_3_1 = OBJECT_END + 22, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_3_3 = OBJECT_END + 24, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_4_1 = OBJECT_END + 25, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_4_3 = OBJECT_END + 27, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_5_1 = OBJECT_END + 28, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_5_3 = OBJECT_END + 30, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_6_1 = OBJECT_END + 31, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_6_3 = OBJECT_END + 33, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_7_1 = OBJECT_END + 34, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_7_3 = OBJECT_END + 36, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_8_1 = OBJECT_END + 37, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_8_3 = OBJECT_END + 39, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_9_1 = OBJECT_END + 40, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_9_3 = OBJECT_END + 42, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_10_1 = OBJECT_END + 43, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_10_3 = OBJECT_END + 45, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_11_1 = OBJECT_END + 46, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_11_3 = OBJECT_END + 48, // 1 2 1
		//ITEM_FIELD_ENCHANTMENT_12_1 = OBJECT_END + 49, // 2 1 1
		//ITEM_FIELD_ENCHANTMENT_12_3 = OBJECT_END + 51, // 1 2 1
		//ITEM_FIELD_PROPERTY_SEED = OBJECT_END + 52, // 1 1 1
		public int PropertySeed { get; set; }
		//ITEM_FIELD_RANDOM_PROPERTIES_ID = OBJECT_END + 53, // 1 1 1
		public int RandomPropertiesID { get; set; }
		//ITEM_FIELD_ITEM_TEXT_ID = OBJECT_END + 54, // 1 1 4
		public int ItemTextID { get; set; }
		//ITEM_FIELD_DURABILITY = OBJECT_END + 55, // 1 1 20
		public int Durability { get; set; }
		//ITEM_FIELD_MAXDURABILITY = OBJECT_END + 56, // 1 1 20
		public int MaxDurability { get; set; }
		//ITEM_FIELD_PAD = OBJECT_END + 57, // 1 1 0
		public int Pad { get { return 0; } }
		//ITEM_END = OBJECT_END + 58,
		#endregion

		public override void Accept(IObjectVisitor visitor) {
			visitor.Visit(this);
		}
	}
}
