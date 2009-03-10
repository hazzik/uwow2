using System;

namespace Hazzik.Objects {
	public partial class Item : WorldObject {
		public Item()
			: this((int)UpdateFields.ITEM_END) {
		}

		protected Item(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Item;
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Item; }
		}
	}
}