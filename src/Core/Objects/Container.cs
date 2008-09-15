using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public class Container : Item {
		public override void Accept(IObjectVisitor visitor) {
			visitor.Visit(this);
		}
	}
}
