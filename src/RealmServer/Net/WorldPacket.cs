using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class WorldPacket : PacketBase, IPacket {
		internal WorldPacket(WMSG code, byte[] data)
			: base((int)code, data) {
		}
		internal WorldPacket(WMSG code)
			: base((int)code) {
		}

		public override void WriteHead(Stream stream) {
			var size = this.Size + 2;
			var code = this.Code;
			stream.WriteByte((byte)(size >> 0x08));
			stream.WriteByte((byte)(size));
			stream.WriteByte((byte)(code));
			stream.WriteByte((byte)(code >> 0x08));
		}

		public override void WriteBody(Stream stream) {
			var bytesRead = 0;
			var buffer = new byte[1024];
			var packetStream = this.GetStream();
			packetStream.Seek(0, SeekOrigin.Begin);
			while((bytesRead = packetStream.Read(buffer, 0, 1024)) > 0) {
				stream.Write(buffer, 0, bytesRead);
			}
		}
	}
}
