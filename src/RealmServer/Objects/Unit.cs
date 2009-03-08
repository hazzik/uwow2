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

		public Races Race {
			get { return (Races)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 0, (byte)value); }
		}

		public Classes Classe {
			get { return (Classes)GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 1); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 1, (byte)value); }
		}

		public int Gender {
			get { return GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 2); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 2, (byte)value); }
		}

		public int ManaType {
			get { return GetByte(UpdateFields.UNIT_FIELD_BYTES_0, 3); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_0, 3, (byte)value); }
		}

		public virtual StandStates StandState {
			get { return (StandStates)GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 0, (byte)value); }
		}

		public BattleStances BattleStance {
			get { return (BattleStances)GetByte(UpdateFields.UNIT_FIELD_BYTES_1, 4); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_1, 4, (byte)value); }
		}

		public SheathType Sheath {
			get { return (SheathType)GetByte(UpdateFields.UNIT_FIELD_BYTES_2, 0); }
			set { SetByte(UpdateFields.UNIT_FIELD_BYTES_2, 0, (byte)value); }
		}
	}
}