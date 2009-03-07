using System;

namespace Hazzik.Objects {
	partial class Container {
		#region CONTAINER_FIELD_NUM_SLOTS
		//CONTAINER_FIELD_NUM_SLOTS : type = Int, size = 1, flag = Public
		public virtual UInt32 NumSlots {
			get { return GetUInt32(UpdateFields.CONTAINER_FIELD_NUM_SLOTS); }
			set { SetUInt32(UpdateFields.CONTAINER_FIELD_NUM_SLOTS, value); }
		}
		#endregion

		//CONTAINER_ALIGN_PAD : type = Bytes, size = 1, flag = None

		//CONTAINER_FIELD_SLOT_1 : type = Long, size = 72, flag = Public
	}
}
