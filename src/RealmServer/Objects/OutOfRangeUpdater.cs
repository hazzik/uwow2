using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hazzik.Objects {
	public class OutOfRangeUpdater : IUpdateBuilder {
		private readonly ICollection<ulong> _guids;

		public OutOfRangeUpdater(ICollection<ulong> guids) {
			_guids = guids;
		}

		public void Write(BinaryWriter writer) {
			writer.Write((byte)UpdateType.OutOfRangeObjects);
			writer.Write(_guids.Count());
			foreach(var guid in _guids) {
				writer.WritePackGuid(guid);
			}
		}

		public bool IsChanged {
			get { return _guids.Count > 0; }
		}
	}
}