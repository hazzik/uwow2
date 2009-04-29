using System;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		public GameObject() {
			Type |= ObjectTypes.GameObject;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.GameObject; }
		}

		public virtual UInt64 Rotation { get; set; }
	}
}