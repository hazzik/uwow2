using System;

namespace Hazzik.Objects {
	public partial class Container : Item {
		public Container()
			: this((int)UpdateFields.CONTAINER_END, 0x07) {
		}

		protected Container(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Container; }
		}
	}
}