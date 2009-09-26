using System;
using System.IO;

namespace Hazzik.Net {
	public abstract class PacketBase : IPacket {
		protected Stream _stream;

		protected internal PacketBase(int code, byte[] data) {
			_stream = new MemoryStream(data, false);
			Code = code;
		}

		protected internal PacketBase(int code) {
			_stream = new MemoryStream();
			Code = code;
		}

		#region IPacket Members

		public int Code { get; protected set; }

		public int Size {
			get { return (int)GetStream().Length; }
		}

		public virtual Stream GetStream() {
			if(_stream == null) {
				_stream = new MemoryStream();
			}
			return _stream;
		}

		public BinaryReader CreateReader() {
			return new BinaryReader(GetStream());
		}

		public BinaryWriter CreateWriter() {
			return new BinaryWriter(GetStream());
		}

		public virtual void WriteBody(Stream stream) {
			int bytesRead;
			var buffer = new byte[1024];
			Stream packetStream = GetStream();
			packetStream.Seek(0, SeekOrigin.Begin);
			while((bytesRead = packetStream.Read(buffer, 0, 1024)) > 0) {
				stream.Write(buffer, 0, bytesRead);
			}
		}

		#endregion
	}
}