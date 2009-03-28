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

		public BitArray Mask {
			get { return _mask; }
		}

		public uint[] Values {
			get { return _values; }
		}

		public bool CheckMask() {
			for(int i = 0; i < Mask.Length; i++) {
				if(Mask[i]) {
					return false;
				}
			}
			return true;
		}

		public void Write(BinaryWriter writer) {
			var length = (byte)GetLengthInDwords(Mask.Length);
			var buffer = new byte[length << 2];
			Mask.CopyTo(buffer, 0);
			writer.Write(length);
			writer.Write(buffer);
			for(int i = 0; i < Mask.Length; i++) {
				if(Mask[i]) {
					writer.Write(Values[i]);
				}
			}
		}

		private static int GetLengthInDwords(int bitsCount) {
			return (bitsCount >> 5) + (bitsCount % 32 != 0 ? 1 : 0);
		}
	}
}