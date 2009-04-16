using System;

namespace Hazzik.Net {
	internal class WorldPacket : PacketBase {
		internal WorldPacket(WMSG code, byte[] data)
			: base((int)code, data) {
		}

		internal WorldPacket(WMSG code)
			: base((int)code) {
		}
	}
}