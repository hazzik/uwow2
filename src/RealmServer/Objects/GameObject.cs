using System;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		public GameObject()
			: this((int)UpdateFields.GAMEOBJECT_END) {
		}

		protected GameObject(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.GameObject;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.GameObject; }
		}
	}
}