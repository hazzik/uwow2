using System.Collections.Generic;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	public class OutOfRangeBlock : IUpdateBlock {
		private readonly ICollection<ulong> _guids;

		public OutOfRangeBlock(ICollection<ulong> guids) {
			_guids = guids;
		}

		#region IUpdateBlock Members

		public void Write(BinaryWriter writer) {
			writer.Write((byte)UpdateType.OutOfRangeObjects);
			writer.Write(_guids.Count);
			foreach(ulong guid in _guids) {
				writer.WritePackGuid(guid);
			}
		}

		public bool IsChanged {
			get { return _guids.Count > 0; }
		}

		#endregion
	}
}