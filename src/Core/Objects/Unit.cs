using System;

namespace Hazzik.Objects {
	public partial class Unit : Mobile {
		public Unit()
			: this((int)UpdateFields.UNIT_END, 0x09) {
		}

		public Unit(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId { get { return (byte)ObjectTypeId.Unit; } }
	}
}