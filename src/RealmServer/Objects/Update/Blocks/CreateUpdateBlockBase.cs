using System;
using System.Collections;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	public abstract class CreateUpdateBlockBase : IUpdateBlock {
		protected bool _isEmpty;
		protected BitArray _mask;
		protected WorldObject _obj;
		protected uint[] _values;

		protected CreateUpdateBlockBase(WorldObject obj, BitArray mask, uint[] values) {
			_obj = obj;
			_mask = mask;
			_values = values;
		}

		#region IUpdateBlock Members

		public abstract bool IsEmpty { get; }

		public abstract UpdateType UpdateType { get; }

		public virtual void Write(BinaryWriter writer) {
			writer.WritePackGuid(_obj.Guid);

			WriteCreateBlock(writer);

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

		#endregion

		private static int GetLengthInDwords(int bitsCount) {
			return (bitsCount >> 5) + (bitsCount % 32 != 0 ? 1 : 0);
		}

		protected abstract void WriteCreateBlock(BinaryWriter writer);
	}
}