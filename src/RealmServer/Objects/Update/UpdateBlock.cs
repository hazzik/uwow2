using System;
using System.Collections;
using System.IO;

namespace Hazzik.Objects.Update {
	public class UpdateBlock : CreateUpdateBlockBase {
		public UpdateBlock(WorldObject obj, BitArray mask, uint[] values) : base(obj, mask, values) {
			_isEmpty = CheckMask();
		}

		public override bool IsEmpty {
			get { return _isEmpty; }
		}

		public override UpdateType UpdateType {
			get { return UpdateType.Values; }
		}

		private bool CheckMask() {
			for(int i = 0; i < _mask.Length; i++) {
				if(_mask[i]) {
					return false;
				}
			}
			return true;
		}

		protected override void WriteCreateBlock(BinaryWriter writer) {
		}
	}
}