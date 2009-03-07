using System;

namespace Hazzik.Objects {
	partial class WorldObject {
		#region OBJECT_FIELD_GUID

		//OBJECT_FIELD_GUID : type = Long, size = 2, flag = Public
		public UInt64 Guid { get { return GetValueUInt64((int)UpdateFields.OBJECT_FIELD_GUID); } set { SetValue((int)UpdateFields.OBJECT_FIELD_GUID, value); } }

		#endregion

		#region OBJECT_FIELD_TYPE

		//OBJECT_FIELD_TYPE : type = Int, size = 1, flag = Public
		public UInt32 Type { get { return GetValueUInt32((int)UpdateFields.OBJECT_FIELD_TYPE); } set { SetValue((int)UpdateFields.OBJECT_FIELD_TYPE, value); } }

		#endregion

		#region OBJECT_FIELD_ENTRY

		//OBJECT_FIELD_ENTRY : type = Int, size = 1, flag = Public
		public UInt32 Entry { get { return GetValueUInt32((int)UpdateFields.OBJECT_FIELD_ENTRY); } set { SetValue((int)UpdateFields.OBJECT_FIELD_ENTRY, value); } }

		#endregion

		#region OBJECT_FIELD_SCALE_X

		//OBJECT_FIELD_SCALE_X : type = Single, size = 1, flag = Public
		public Single ScaleX { get { return GetValueSingle((int)UpdateFields.OBJECT_FIELD_SCALE_X); } set { SetValue((int)UpdateFields.OBJECT_FIELD_SCALE_X, value); } }

		#endregion

		//OBJECT_FIELD_PADDING : type = Int, size = 1, flag = None
	}
}
