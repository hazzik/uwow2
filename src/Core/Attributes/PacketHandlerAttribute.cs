using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class PacketHandlerAttribute : BaseHandlerAttribute {
		public PacketHandlerAttribute(int code)
			: base(code) {
		}
	}
}