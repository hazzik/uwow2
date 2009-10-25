using System;
using System.IO;
using Hazzik.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class CreateBlockWriter : IUpdateBlock {
		private readonly UpdateValuesDto dto;
		private readonly WorldObject obj;
		private readonly bool self;

		public CreateBlockWriter(bool self, WorldObject obj, UpdateValuesDto dto) {
			this.self = self;
			this.obj = obj;
			this.dto = dto;
		}

		#region IUpdateBlock Members

		public bool IsEmpty {
			get { return false; }
		}

		public UpdateType UpdateType {
			get { return self ? UpdateType.CreateObject2 : UpdateType.CreateObject; }
		}

		public void Write(BinaryWriter writer) {
			writer.WritePackGuid(obj.Guid);

			obj.WriteCreateBlock(self, writer);

			dto.Write(writer);
		}

		#endregion
	}
}