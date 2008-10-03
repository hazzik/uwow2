using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class PacketHandlerAttribute : BaseHandlerAttribute {
		public PacketHandlerAttribute(int code)
			: base(code) {
		}
	}
}
