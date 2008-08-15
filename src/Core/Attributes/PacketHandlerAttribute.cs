using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	public class PacketHandlerAttribute : BaseHandlerAttribute {
		public PacketHandlerAttribute(int code)
			: base(code) {
		}
	}
}
