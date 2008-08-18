using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AuthPacketHandlerAttribute : PacketHandlerAttribute {
		public AuthPacketHandlerAttribute(RMSG msg)
			: base((int)msg) {

		}
	}
}
