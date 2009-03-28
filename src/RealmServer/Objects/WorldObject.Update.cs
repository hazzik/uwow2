using System;

namespace Hazzik.Objects {
	partial class WorldObject {
		#region OBJECT_FIELD_GUID

		//OBJECT_FIELD_GUID : type = Long, size = 2, flag = Public
		public virtual UInt64 Guid { get; set; }

		#endregion

		#region OBJECT_FIELD_TYPE

		//OBJECT_FIELD_TYPE : type = Int, size = 1, flag = Public
		public virtual ObjectTypes Type { get; set; }

		#endregion

		#region OBJECT_FIELD_ENTRY

		//OBJECT_FIELD_ENTRY : type = Int, size = 1, flag = Public
		public virtual UInt32 Entry { get; set; }

		#endregion

		#region OBJECT_FIELD_SCALE_X

		//OBJECT_FIELD_SCALE_X : type = Single, size = 1, flag = Public
		public virtual Single ScaleX { get; set; }

		#endregion

		//OBJECT_FIELD_PADDING : type = Int, size = 1, flag = None
	}
}