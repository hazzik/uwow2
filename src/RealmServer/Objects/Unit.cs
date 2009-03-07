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
			get { return (Races)GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_0, 0); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_0, 0, (byte)value); }
		}

		public Classes Classe {
			get { return (Classes)GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_0, 1); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_0, 1, (byte)value); }
		}

		public int Gender {
			get { return GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_0, 2); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_0, 2, (byte)value); }
		}

		public int ManaType {
			get { return GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_0, 3); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_0, 3, (byte)value); }
		}

		public StandStates StandState {
			get { return (StandStates)GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_1, 0); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_1, 0, (byte)value); }
		}

		public BattleStances BattleStance {
			get { return (BattleStances)GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_1, 4); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_1, 4, (byte)value); }
		}

		public SheathType Sheath {
			get { return (SheathType)GetValueByte((int)UpdateFields.UNIT_FIELD_BYTES_2, 0); }
			set { SetValue((int)UpdateFields.UNIT_FIELD_BYTES_2, 0, (byte)value); }
		}
	}
}