using System;
using System.Collections;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	public class UpdateBlock {
		private readonly BitArray _mask;
		private readonly uint[] _values;

		public UpdateBlock(BitArray mask, uint[] values) {
			_mask = mask;
			_values = values;
		}

		public bool CheckEmpty() {
			for(int i = 0; i < _mask.Length; i++) {
				if(_mask[i]) {
					return false;
				}
			}
			return true;
		}

		public void Write(BinaryWriter writer) {
			var length = (byte)GetLengthInDwords(_mask.Length);
			var buffer = new byte[length << 2];
			_mask.CopyTo(buffer, 0);
			writer.Write(length);
			writer.Write(buffer);
			for(int i = 0; i < _mask.Length; i++) {
				if(_mask[i]) {
					writer.Write(_values[i]);
				}
			}
		}

		private static int GetLengthInDwords(int bitsCount) {
			return (bitsCount >> 5) + (bitsCount % 32 != 0 ? 1 : 0);
		}
	}
}