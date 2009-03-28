using System;

namespace Hazzik.Objects {
	public partial class DynamicObject : Positioned {
		public DynamicObject() {
			Type |= ObjectTypes.DynamicObject;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.DynamicObject; }
		}
	}
}