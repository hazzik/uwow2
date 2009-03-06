using System;
using System.IO;

namespace Hazzik.Objects {
	public abstract class Positioned : WorldObject {
		protected Positioned(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte UpdateFlag {
			get { return (byte)(base.UpdateFlag | (byte)UpdateFlags.HasPosition); }
		}

		public virtual float X { get; set; }
		public virtual float Y { get; set; }
		public virtual float Z { get; set; }
		public virtual float O { get; set; }

		public override void WriteCreateBlock(BinaryWriter writer) {
			writer.Write(X);
			writer.Write(Y);
			writer.Write(Z);
			writer.Write(O);
		}
	}
}