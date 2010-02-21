using System;
using System.IO;
using Hazzik.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class UpdateBlockWriter : IUpdateBlock {
		private readonly UpdateValuesDto dto;
		private readonly ulong guid;
		private readonly bool isEmpty;

		public UpdateBlockWriter(ulong guid, UpdateValuesDto dto) {
			this.guid = guid;
			this.dto = dto;
			isEmpty = !dto.Dirty;
		}

		#region IUpdateBlock Members

		public bool IsEmpty {
			get { return isEmpty; }
		}

		public UpdateType UpdateType {
			get { return UpdateType.Values; }
		}

		public void Write(BinaryWriter writer) {
			writer.WritePackGuid(guid);
			dto.Write(writer);
		}

		#endregion
	}
}