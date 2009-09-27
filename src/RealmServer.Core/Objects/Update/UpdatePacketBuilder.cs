using System;
using System.Collections.Generic;
using System.IO;
using Hazzik.Net;

namespace Hazzik.Objects.Update {
	internal class UpdatePacketBuilder : IPacketBuilder {
		private readonly ICollection<IUpdateBlock> updateBlocks;

		public UpdatePacketBuilder(ICollection<IUpdateBlock> updateBlocks) {
			this.updateBlocks = updateBlocks;
		}

		#region IPacketBuilder Members

		public bool IsEmpty {
			get { return updateBlocks.Count == 0; }
		}

		public IPacket Build() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_UPDATE_OBJECT);
			BinaryWriter writer = result.CreateWriter();
			writer.Write(updateBlocks.Count);
			foreach(IUpdateBlock updater in updateBlocks) {
				writer.Write((byte)updater.UpdateType);
				updater.Write(writer);
			}
			return result;
		}

		#endregion
	}
}