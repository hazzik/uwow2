using System;

namespace Hazzik.Objects {
	public partial class Corpse : Positioned {
		public Corpse()
			: this((int)UpdateFields.CORPSE_END, 0x81) {
		}

		protected Corpse(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Corpse; }
		}
	}
}