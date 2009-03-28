using System;
using System.Collections;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class CreateBlock : CreateUpdateBlockBase {
		protected bool _self;

		public CreateBlock(bool self, UpdateObjectDto obj, BitArray mask, uint[] values)
			: base(true, obj, mask, values) {
			_self = self;
		}

		public override bool IsEmpty {
			get { return false; }
		}

		public override UpdateType UpdateType {
			get { return _self ? UpdateType.CreateObject2 : UpdateType.CreateObject; }
		}

		public void WriteCreateBlock(BinaryWriter writer) {
			(_obj.Object).WriteCreateBlock(_self, writer);
		}
	}
}