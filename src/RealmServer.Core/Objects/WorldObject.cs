using System;
using System.IO;
using Hazzik.IO;
using Hazzik.Objects.Update;

namespace Hazzik.Objects {
	public abstract partial class WorldObject {
		protected WorldObject() {
			Guid = ObjectGuid.NewGuid();
			Type = ObjectTypes.Object;
			ScaleX = 1f;
		}

		public abstract ObjectTypeId TypeId { get; }

		public virtual UpdateFlags UpdateFlag {
			get { return UpdateFlags.HighGuid | UpdateFlags.LowGuid; }
		}

		protected virtual void WriteCreateBlock(BinaryWriter writer) {
		}

		public void WriteCreateBlock(bool self, BinaryWriter writer) {
			writer.Write((byte)TypeId);
			writer.Write((ushort)(!self ? UpdateFlag : UpdateFlag | UpdateFlags.Self));
			WriteCreateBlock(writer);
			if(UpdateFlag.Has(UpdateFlags.LowGuid)) {
				writer.Write((uint)0x00);
			}
			if(UpdateFlag.Has(UpdateFlags.HighGuid)) {
				writer.Write((uint)0x00);
			}
			if(UpdateFlag.Has(UpdateFlags.TargetGuid)) {
				writer.WritePackGuid(0x00);
			}
			if(UpdateFlag.Has(UpdateFlags.Transport)) {
				writer.Write((uint)0x00);
			}
		}
	}
}