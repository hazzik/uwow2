using System;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class BaseHandlerAttribute : Attribute {
		public int Code { get; private set; }

		public BaseHandlerAttribute(int code) {
			Code = code;
		}
	}
}