using System;
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

		public UpdateType UpdateType {
			get { return UpdateType.OutOfRangeObjects; }
		}

		public void Write(BinaryWriter writer) {
			writer.Write(_guids.Count);
			foreach(ulong guid in _guids) {
				writer.WritePackGuid(guid);
			}
		}

		public bool IsEmpty {
			get { return _guids.Count == 0; }
		}

		#endregion
	}
}