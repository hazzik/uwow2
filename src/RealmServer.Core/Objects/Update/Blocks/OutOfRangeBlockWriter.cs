using System;
using System.Collections.Generic;
using System.IO;

namespace Hazzik.Objects.Update.Blocks {
	internal class OutOfRangeBlockWriter : IUpdateBlock {
		private readonly ICollection<ulong> guids;

		public OutOfRangeBlockWriter(ICollection<ulong> guids) {
			this.guids = guids;
		}

		#region IUpdateBlock Members

		public UpdateType UpdateType {
			get { return UpdateType.OutOfRangeObjects; }
		}

		public void Write(BinaryWriter writer) {
			writer.Write(guids.Count);
			foreach(ulong guid in guids) {
				writer.WritePackGuid(guid);
			}
		}

		public bool IsEmpty {
			get { return guids.Count == 0; }
		}

		#endregion
	}
}