using System;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class CreateBlockWriter : IUpdateBlock {
		private readonly bool _self;
		private readonly WorldObject _obj;
		private readonly UpdateBlock _updateBlock;

		public CreateBlockWriter(bool self, WorldObject obj, UpdateBlock updateBlock) {
			_self = self;
			_obj = obj;
			_updateBlock = updateBlock;
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

			_updateBlock.Write(writer);
		}

		#endregion
	}
}