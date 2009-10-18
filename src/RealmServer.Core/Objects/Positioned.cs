using System;
using System.IO;
using Hazzik.Objects.Update;

namespace Hazzik.Objects {
	public abstract class Positioned : WorldObject {
		public override UpdateFlags UpdateFlag {
			get { return (base.UpdateFlag | UpdateFlags.HasPosition); }
		}

		public virtual uint MapId { get; set; }
		public virtual uint ZoneId { get; set; }
		public virtual float PosX { get; set; }
		public virtual float PosY { get; set; }
		public virtual float PosZ { get; set; }
		public virtual float Facing { get; set; }

		protected override void WriteCreateBlock(BinaryWriter writer) {
			writer.Write(PosX);
			writer.Write(PosY);
			writer.Write(PosZ);
			writer.Write(Facing);
		}

		public virtual bool IsSeenBy(Player player) {
			return true;
		}
	}
}