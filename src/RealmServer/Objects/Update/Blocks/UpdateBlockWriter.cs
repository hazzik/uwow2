using System;
using System.Collections;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class UpdateBlockWriter : IUpdateBlock {
		protected bool _create;
		private readonly ulong _guid;
		protected UpdateBlock _updateBlock;
		protected bool _isEmpty;

		public UpdateBlockWriter(ulong guid, UpdateBlock updateBlock) {
			_isEmpty = _updateBlock.CheckMask();
			_create = false;
			_guid = guid;
			_updateBlock = updateBlock;
		}

		public bool IsEmpty {
			get { return _isEmpty; }
		}

		public UpdateType UpdateType {
			get { return UpdateType.Values; }
		}

		public void Write(BinaryWriter writer) {
			writer.WritePackGuid(_guid);
			_updateBlock.Write(writer);
		}
	}
}