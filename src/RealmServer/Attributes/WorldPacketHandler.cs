using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class WorldPacketHandlerAttribute : PacketHandlerAttribute {
		public WorldPacketHandlerAttribute(WMSG code)
			: base((int)code) {
		}
	}
}