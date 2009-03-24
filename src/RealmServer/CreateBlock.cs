using System;
using System.Collections;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	public class CreateBlock : CreateUpdateBlockBase {
		protected bool _self;

		public CreateBlock(bool self, WorldObject obj, BitArray mask, uint[] values)
			: base(obj, mask, values) {
			_self = self;
		}

		public override bool IsEmpty {
			get { return false; }
		}

		public override UpdateType UpdateType {
			get { return _self ? UpdateType.CreateObject2 : UpdateType.CreateObject; }
		}

		protected override void WriteCreateBlock(BinaryWriter writer) {
			writer.Write((byte)_obj.TypeId);
			writer.Write((byte)(!_self ? _obj.UpdateFlag : _obj.UpdateFlag | UpdateFlags.Self));
			_obj.WriteCreateBlock(writer);
			if(_obj.UpdateFlag.Has(UpdateFlags.HighGuid)) {
				writer.Write((uint)0x00);
			}
			if(_obj.UpdateFlag.Has(UpdateFlags.LowGuid)) {
				writer.Write((uint)0x00);
			}
			if(_obj.UpdateFlag.Has(UpdateFlags.TargetGuid)) {
				writer.WritePackGuid(0x00);
			}
			if(_obj.UpdateFlag.Has(UpdateFlags.Transport)) {
				writer.Write((uint)0x00);
			}
		}
	}
}