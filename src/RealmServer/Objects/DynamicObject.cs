using System;

namespace Hazzik.Objects {
	public partial class DynamicObject : Positioned {
		public DynamicObject()
			: this((int)UpdateFields.DYNAMICOBJECT_END, 0x41) {
		}

		protected DynamicObject(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.DynamicObject; }
		}
	}
}