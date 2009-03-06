using System.IO;

namespace Hazzik.Objects {
	public abstract class Mobile : Positioned {
		protected Mobile(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte UpdateFlag { get { return (byte)(base.UpdateFlag | (byte)UpdateFlags.Mobile); } }

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
			w.Write((ushort)0); //всегда 0
			w.Write(0xDD57244F); //аптайм сервера

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
			w.Write(TurnRate);
		}
	}
}