using System;
using System.Collections.Generic;
using System.IO;

namespace Hazzik.Objects {
	public class OutOfRangeUpdater : IUpdateBuilder {
		private readonly ICollection<ulong> _guids;

		public OutOfRangeUpdater(ICollection<ulong> guids) {
			_guids = guids;
		}

		#region IUpdateBuilder Members

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