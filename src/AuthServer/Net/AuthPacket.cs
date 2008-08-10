using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class AuthPacket : IPacket {
		private Stream _stream;

		internal AuthPacket(int code, byte[] data) {
			_stream = new MemoryStream(data, false);
			this.Code = code;
		}

		internal AuthPacket(int code) {
			this.Code = code;
		}

		public int Code { get; set; }

		public int Size { get { return (int)_stream.Length; } }

		public Stream GetStream() {
			if(_stream == null) {
				_stream = new MemoryStream();
			}
			return _stream;
		}
	}
}
