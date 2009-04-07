using System;
using System.Collections.Generic;
using Hazzik.Net;

namespace Hazzik.Objects.Update {
	internal class UpdatePacketBuilder {
		private readonly ICollection<IUpdateBlock> _updateBlocks;

		public UpdatePacketBuilder(ICollection<IUpdateBlock> updateBlocks) {
			_updateBlocks = updateBlocks;
		}

		public IPacket Build() {
			var result = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
			var writer = result.CreateWriter();
			writer.Write(_updateBlocks.Count);
			foreach(var updater in _updateBlocks) {
				writer.Write((byte)updater.UpdateType);
				updater.Write(writer);
			}
			return result;
		}
	}
}