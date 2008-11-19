using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AuthPacketHandlerAttribute : PacketHandlerAttribute {
		public AuthPacketHandlerAttribute(RMSG msg)
			: base((int)msg) {
		}
	}
}