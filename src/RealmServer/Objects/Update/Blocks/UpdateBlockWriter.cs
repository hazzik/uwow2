using System;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class UpdateBlockWriter : IUpdateBlock {
		private readonly UpdateValuesDto _dto;
		private readonly ulong _guid;
		private readonly bool _isEmpty;
		protected bool _create;

		public UpdateBlockWriter(ulong guid, UpdateValuesDto dto) {
			_create = false;
			_guid = guid;
			_isEmpty = !dto.HasChanges;
			_dto = dto;
		}

		#region IUpdateBlock Members

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

		#endregion
	}
}