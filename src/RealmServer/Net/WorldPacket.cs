using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hazzik.Net {
	public class WorldPacket : PacketBase, IPacket {
		internal WorldPacket(int code, byte[] data)
			: base(code, data) {
		}
		internal WorldPacket(int code)
			: base(code) {
		}
	}
}
