using System;
using System.Collections.Generic;
using System.IO;
using Hazzik.Net;

namespace Hazzik.Objects.Update {
	internal class UpdatePacketBuilder : IPacketBuilder {
		private readonly ICollection<IUpdateBlock> _updateBlocks;

		public UpdatePacketBuilder(ICollection<IUpdateBlock> updateBlocks) {
			_updateBlocks = updateBlocks;
		}

		#region IPacketBuilder Members

		public bool IsEmpty {
			get { return _updateBlocks.Count == 0; }
		}

		public IPacket Build() {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_UPDATE_OBJECT);
			BinaryWriter writer = result.CreateWriter();
			writer.Write(_updateBlocks.Count);
			foreach(IUpdateBlock updater in _updateBlocks) {
				writer.Write((byte)updater.UpdateType);
				updater.Write(writer);
			}
			return result;
		}

		#endregion
	}
}