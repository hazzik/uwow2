using System;

namespace Hazzik.Objects {
	public partial class GameObject : Positioned {
		public GameObject()
			: this((int)UpdateFields.GAMEOBJECT_END, 0x21) {
		}

		protected GameObject(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.GameObject; }
		}

		public override byte UpdateFlag {
			get { return (byte)(UpdateFlags.LowGuid | UpdateFlags.HighGuid); }
		}
	}
}