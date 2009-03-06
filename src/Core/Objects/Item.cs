using System;

namespace Hazzik.Objects {
	public partial class Item : WorldObject {
		public Item()
			: this((int)UpdateFields.ITEM_END, 0x03) {
		}

		protected Item(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Item; }
		}
	}
}