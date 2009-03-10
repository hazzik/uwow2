using System;
using System.IO;

namespace Hazzik.Objects {
	public abstract class Positioned : WorldObject {
		protected Positioned(int updateMaskLength)
			: base(updateMaskLength) {
		}


		public override byte UpdateFlag {
			get { return (byte)(base.UpdateFlag | (byte)UpdateFlags.HasPosition); }
		}

		public virtual uint MapId { get; set; }
		public virtual uint ZoneId { get; set; }
		public virtual float PosX { get; set; }
		public virtual float PosY { get; set; }
		public virtual float PosZ { get; set; }
		public virtual float Facing { get; set; }

		public override void WriteCreateBlock(BinaryWriter writer) {
			writer.Write(PosX);
			writer.Write(PosY);
			writer.Write(PosZ);
			writer.Write(Facing);
		}
	}
}