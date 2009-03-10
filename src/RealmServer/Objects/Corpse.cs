using System;

namespace Hazzik.Objects {
	public partial class Corpse : Positioned {
		public Corpse()
			: this((int)UpdateFields.CORPSE_END) {
		}

		protected Corpse(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Corpse;
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Corpse; }
		}
	}
}