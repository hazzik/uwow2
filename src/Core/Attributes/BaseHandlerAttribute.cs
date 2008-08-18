using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class BaseHandlerAttribute : Attribute {
		public int Code { get; private set; }

		public BaseHandlerAttribute(int code) {
			this.Code = code;
		}
	}
}
