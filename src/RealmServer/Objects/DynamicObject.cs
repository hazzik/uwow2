using System;

namespace Hazzik.Objects {
	public partial class DynamicObject : Positioned {
		public DynamicObject()
			: this((int)UpdateFields.DYNAMICOBJECT_END) {
		}

		protected DynamicObject(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.DynamicObject;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.DynamicObject; }
		}
	}
}