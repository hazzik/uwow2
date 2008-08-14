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
	}
}
