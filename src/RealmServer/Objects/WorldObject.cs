using System.IO;
using Hazzik.Net;
using Hazzik.Objects.Update;

namespace Hazzik.Objects {
	public abstract partial class WorldObject : UpdateObjectDto {
		protected WorldObject(int maxValues)
			: base(maxValues) {
			Guid = ObjectGuid.NewGuid();
			Type = ObjectTypes.Object;
			ScaleX = 1f;
		}

		public abstract ObjectTypeId TypeId { get; }

		public virtual UpdateFlags UpdateFlag {
			get { return UpdateFlags.HighGuid | UpdateFlags.LowGuid; }
		}

		public virtual void WriteCreateBlock(BinaryWriter writer) {

		}

		public IPacket GetDestroyObjectPkt() {
			var result = new WorldPacket(WMSG.SMSG_DESTROY_OBJECT);
			var writer = result.CreateWriter();
			writer.Write(Guid);
			return result;
		}
	}
}