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