using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	public class WorldPacketHandlerAttribute : PacketHandlerAttribute {
		public WorldPacketHandlerAttribute(WMSG code)
			: base((int)code) {
		}
	}
}
