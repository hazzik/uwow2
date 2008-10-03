using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public abstract class PacketBase : IPacket {
		protected Stream _stream;

		protected internal PacketBase(int code, byte[] data) {
			_stream = new MemoryStream(data, false);
			this.Code = code;
		}

		protected internal PacketBase(int code) {
			_stream = new MemoryStream();
			this.Code = code;
		}

		public int Code { get; protected set; }

		public int Size { get { return (int)GetStream().Length; } }

		public virtual Stream GetStream() {
			if(_stream == null) {
				_stream = new MemoryStream();
			}
			return _stream;
		}

		public BinaryReader CreateReader() {
			return new BinaryReader(this.GetStream());
		}

		public BinaryWriter CreateWriter() {
			return new BinaryWriter(this.GetStream());
		}

		public abstract void WriteHead(Stream stream);
		public abstract void WriteBody(Stream stream);
	}
}
