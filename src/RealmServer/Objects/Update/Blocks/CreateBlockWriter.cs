using System;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class CreateBlockWriter : IUpdateBlock {
		private readonly bool _self;
		private readonly WorldObject _obj;
		private readonly UpdateValuesDto _dto;

		public CreateBlockWriter(bool self, WorldObject obj, UpdateValuesDto dto) {
			_self = self;
			_obj = obj;
			_dto = dto;
		}

		#region IUpdateBlock Members

		public bool IsEmpty {
			get { return false; }
		}

		public UpdateType UpdateType {
			get { return _self ? UpdateType.CreateObject2 : UpdateType.CreateObject; }
		}

		public void Write(BinaryWriter writer) {
			writer.WritePackGuid(_obj.Guid);

			_obj.WriteCreateBlock(_self, writer);

			_dto.Write(writer);
		}

		#endregion
	}
}