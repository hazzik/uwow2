using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Attributes {
	public class UpdateValueAttribute : Attribute {
		public int Field { get; set; }
	}
}
