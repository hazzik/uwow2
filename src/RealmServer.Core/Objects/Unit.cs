using System;
using System.IO;
using Hazzik.Objects.Update;

namespace Hazzik.Objects {
	public partial class Unit : Positioned {
		private readonly MovementInfo movementInfo = new MovementInfo();

		public Unit() {
			_attackPower = AttackPowerCalculatorFactory.CreateAttackPowerCalculator(this);
			Type |= ObjectTypes.Unit;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Unit; }
		}

		public override UpdateFlags UpdateFlag {
			get { return (base.UpdateFlag | UpdateFlags.Mobile); }
		}

		public override float PosX {
			get { return movementInfo.X; }
			set { movementInfo.X = value; }
		}

		public override float PosY {
			get { return movementInfo.Y; }
			set { movementInfo.Y = value; }
		}

		public override float PosZ {
			get { return movementInfo.Z; }
			set { movementInfo.Z = value; }
		}

		public override float Facing {
			get { return movementInfo.O; }
			set { movementInfo.O = value; }
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
			get { return movementInfo; }
		}

		public string Name { get; set; }

		protected override void WriteCreateBlock(BinaryWriter w) {
			movementInfo.Write(w);
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