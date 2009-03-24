using System.Collections.Generic;
using Hazzik.Net;

namespace Hazzik {
	public class UpdatePacketBuilder {
		private readonly ICollection<IUpdateBlock> _updaters;

		public UpdatePacketBuilder(ICollection<IUpdateBlock> updaters) {
			_updaters = updaters;
		}

		public IPacket Build() {
			var result = new WorldPacket(WMSG.SMSG_UPDATE_OBJECT);
			var writer = result.CreateWriter();
			writer.Write(_updaters.Count);
			foreach(var updater in _updaters) {
				writer.Write((byte)updater.UpdateType);
				updater.Write(writer);
			}
			return result;
		}
	}
}