using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class AuthPacketIn : IPacket {
		byte[] _data;

		internal AuthPacketIn(int code, byte[] data) {
			_data = data;
			this.Code = code;
		}

		public int Code { get; set; }

		public int Size { get { return _data.Length; } }

		public Stream GetStream() {
			return new MemoryStream(_data);
		}
	}
}
