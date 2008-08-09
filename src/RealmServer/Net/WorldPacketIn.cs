using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class WorldPacketIn : IPacket {
		byte[] _array;

		internal WorldPacketIn(int code, byte[] data) {
			_array = data;
			this.Code = Code;
		}

		public int Code { get; set; }

		public int Size { get { return _array.Length; } }

		public Stream GetStream() {
			return new MemoryStream(_array);
		}
	}
}
