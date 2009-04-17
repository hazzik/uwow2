using System;

namespace Hazzik.Net {
	public class WorldPacketFactory {
		public static IPacket Create(WMSG code) {
			return new WorldPacket(code);
		}
	}
}