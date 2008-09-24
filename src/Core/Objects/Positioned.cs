using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public override void WriteCreateBlock(BinaryWriter w) {
			w.Write(X);
			w.Write(Y);
			w.Write(Z);
			w.Write(O);
		}
	}
}
