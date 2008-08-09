using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Hazzik {
	public abstract class WorldObject {

		private BitArray _updateMask;

		public WorldObject(int updateMaskLength) {
			_updateMask = new BitArray(updateMaskLength);
		}

		//OBJECT_FIELD_GUID = 0, // 2 4 1
		public virtual long Guid { get; set; }
		//OBJECT_FIELD_TYPE = 2, // 1 1 1
		public virtual int Type { get; set; }
		//OBJECT_FIELD_ENTRY = 3, // 1 1 1
		public virtual int Entry { get; set; }
		//OBJECT_FIELD_SCALE_X = 4, // 1 3 1
		public virtual float ScaleX { get; set; }
		//OBJECT_FIELD_PADDING = 5, // 1 1 0
		public virtual int Pad { get { return 0; } }

		protected void UpdateValue(UpdateFields field) {
			_updateMask[(int)field] = true;
		}
	}
}
