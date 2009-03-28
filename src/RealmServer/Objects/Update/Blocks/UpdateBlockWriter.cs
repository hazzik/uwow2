using System;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class UpdateBlockWriter : IUpdateBlock {
		protected bool _create;
		private readonly ulong _guid;
		private readonly UpdateBlock _updateBlock;
		private readonly bool _isEmpty;

		public UpdateBlockWriter(ulong guid, UpdateBlock updateBlock) {
			_create = false;
			_guid = guid;
			_updateBlock = updateBlock;
			_isEmpty = updateBlock.CheckEmpty();
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