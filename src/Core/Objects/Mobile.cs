using System;
using System.IO;

namespace Hazzik.Objects {
	[Flags]
	public enum MovementFlags {
		NONE = 0x00000000,
		FORWARD = 0x00000001,
		BACKWARD = 0x00000002,
		STRAFE_LEFT = 0x00000004,
		STRAFE_RIGHT = 0x00000008,
		LEFT = 0x00000010,
		RIGHT = 0x00000020,
		PITCH_UP = 0x00000040,
		PITCH_DOWN = 0x00000080,
		WALK = 0x00000100,
		ONTRANSPORT = 0x00000200,
		UNK1 = 0x00000400,
		FLY_UNK1 = 0x00000800,
		JUMPING = 0x00001000,
		UNK4 = 0x00002000,
		FALLING = 0x00004000,
		// 0x8000, 0x10000, 0x20000, 0x40000, 0x80000, 0x100000
		SWIMMING = 0x00200000, // appears with fly flag also
		FLY_UP = 0x00400000,
		CAN_FLY = 0x00800000,
		FLYING = 0x01000000,
		UNK5 = 0x02000000,
		SPLINE = 0x04000000, // probably wrong name
		SPLINE2 = 0x08000000,
		WATERWALKING = 0x10000000,
		SAFE_FALL = 0x20000000, // active rogue safe fall spell (passive)
		UNK3 = 0x40000000,
	}

	public abstract class Mobile : Positioned {
		protected Mobile(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte UpdateFlag {
			get { return (byte)(base.UpdateFlag | (byte)UpdateFlags.Mobile); }
		}

		public uint MovementFlag { get; set; }

		public float Speed0 { get; set; }
		public float Speed1 { get; set; }
		public float Speed2 { get; set; }
		public float Speed3 { get; set; }
		public float Speed4 { get; set; }
		public float Speed5 { get; set; }
		public float Speed6 { get; set; }
		public float TurnRate { get; set; }

		public override void WriteCreateBlock(BinaryWriter w) {
			w.Write(MovementFlag);
			w.Write((byte)0); //всегда 0
			w.Write((uint)0); //аптайм сервера

			base.WriteCreateBlock(w);
			WriteMovementBlock(w);
		}

		private void WriteMovementBlock(BinaryWriter w) {
			w.Write((uint)0); // похоже на время
			w.Write(Speed0);
			w.Write(Speed1);
			w.Write(Speed2);
			w.Write(Speed3);
			w.Write(Speed4);
			w.Write(Speed5);
			w.Write(Speed6);
			w.Write(TurnRate);
		}
	}
}