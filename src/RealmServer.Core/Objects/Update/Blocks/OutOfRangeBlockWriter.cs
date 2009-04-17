using System;
using System.Collections.Generic;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class OutOfRangeBlockWriter : IUpdateBlock {
		private readonly ICollection<ulong> _guids;

		public OutOfRangeBlockWriter(ICollection<ulong> guids) {
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