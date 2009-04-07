using System;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class UpdateBlockWriter : IUpdateBlock {
		protected bool _create;
		private readonly ulong _guid;
		private readonly bool _isEmpty;
		private readonly UpdateValuesDto _dto;

		public UpdateBlockWriter(ulong guid, UpdateValuesDto dto) {
			_create = false;
			_guid = guid;
			_isEmpty = !dto.HasChanges;
			_dto = dto;
		}

		public bool IsEmpty {
			get { return _isEmpty; }
		}

		public UpdateType UpdateType {
			get { return UpdateType.Values; }
		}

		public void Write(BinaryWriter writer) {
			writer.WritePackGuid(_guid);
			_dto.Write(writer);
		}
	}
}