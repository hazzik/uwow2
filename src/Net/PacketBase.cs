using System;
using System.IO;

namespace Hazzik.Net {
	public abstract class PacketBase : IPacket {
		protected Stream stream;

		protected PacketBase(int code, byte[] data) {
			stream = new MemoryStream(data, false);
			Code = code;
		}

		protected PacketBase(int code) {
			stream = new MemoryStream();
			Code = code;
		}

		#region IPacket Members

		public int Code { get; private set; }

		public int Size {
			get { return (int)GetStream().Length; }
		}

		public virtual Stream GetStream() {
			return stream ?? (stream = new MemoryStream());
		}

		public BinaryReader CreateReader() {
			return new BinaryReader(GetStream());
		}

		public BinaryWriter CreateWriter() {
			return new BinaryWriter(GetStream());
		}

		public virtual void WriteBody(Stream to) {
			int bytesRead;
			var buffer = new byte[1024];
			Stream packetStream = GetStream();
			packetStream.Seek(0, SeekOrigin.Begin);
			while((bytesRead = packetStream.Read(buffer, 0, 1024)) > 0) {
				to.Write(buffer, 0, bytesRead);
			}
		}

		#endregion
	}
}