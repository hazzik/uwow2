using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class AuthPacket : PacketBase, IPacket {

		internal AuthPacket(RMSG code, byte[] data)
			: base((int)code, data) {
		}

		internal AuthPacket(RMSG code)
			: base((int)code) {
		}

		public override void WriteHead(Stream stream) {
			stream.WriteByte((byte)this.Code);
			if((RMSG)this.Code == RMSG.REALM_LIST || (RMSG)this.Code == RMSG.XFER_DATA) {
				stream.WriteByte((byte)(this.Size));
				stream.WriteByte((byte)(this.Size >> 0x08));
			}
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
