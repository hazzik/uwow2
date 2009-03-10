using System;
using System.IO;

namespace Hazzik.Objects {
	public partial class Unit : Positioned {
		private readonly MovementInfo _movementInfo = new MovementInfo();

		public Unit()
			: this((int)UpdateFields.UNIT_END) {
		}

		public Unit(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Unit;
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

		public override byte UpdateFlag {
			get { return (byte)(base.UpdateFlag | (byte)UpdateFlags.Mobile); }
		}

		public override float PosX {
			get { return _movementInfo.X; }
			set { _movementInfo.X = value; }
		}

		public override float PosY {
			get { return _movementInfo.Y; }
			set { _movementInfo.Y = value; }
		}

		public override float PosZ {
			get { return _movementInfo.Z; }
			set { _movementInfo.Z = value; }
		}

		public override float Facing {
			get { return _movementInfo.O; }
			set { _movementInfo.O = value; }
		}

		public float Speed0 { get; set; }
		public float Speed1 { get; set; }
		public float Speed2 { get; set; }
		public float Speed3 { get; set; }
		public float Speed4 { get; set; }
		public float Speed5 { get; set; }
		public float Speed6 { get; set; }
		public float TurnRate { get; set; }

		public MovementInfo MovementInfo {
			get { return _movementInfo; }
		}

		public override void WriteCreateBlock(BinaryWriter w) {
			_movementInfo.Write(w);
			w.Write(Speed0);
			w.Write(Speed1);
			w.Write(Speed2);
			w.Write(Speed3);
			w.Write(Speed4);
			w.Write(Speed5);
			w.Write(Speed6);
			w.Write(TurnRate);
			w.Write(TurnRate);
		}
	}
}