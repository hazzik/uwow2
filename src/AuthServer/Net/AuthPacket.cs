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
	}
}
