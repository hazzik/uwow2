using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class PacketHandlerAttribute : Attribute {
		protected PacketHandlerAttribute(int code) {
			Code = code;
		}

		public int Code { get; private set; }
	}
}