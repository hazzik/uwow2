using System;

namespace Hazzik.Attributes {
	public class UpdateValueAttribute : Attribute {
		public int Field { get; set; }
	}
}