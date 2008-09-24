using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public class Container : Item {
		public Container()
			: this((int)UpdateFields.CONTAINER_END, 0x07) {
		}

		protected Container(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Container; }
		}

		public override byte UpdateFlag {
			get { return (byte)(UpdateFlags.LowGuid | UpdateFlags.HighGuid); }
		}

		public override void Accept(IObjectVisitor visitor) {
			visitor.Visit(this);
		}
	}
}
