using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public partial class GameObject {
		#region OBJECT_FIELD_CREATED_BY
		//OBJECT_FIELD_CREATED_BY : type = Long, size = 2, flag = Public
		public virtual UInt64 CreatedByGuid {
			get { return GetUInt64(UpdateFields.OBJECT_FIELD_CREATED_BY); }
			set { SetUInt64(UpdateFields.OBJECT_FIELD_CREATED_BY, value); }
		}
		#endregion

		#region GAMEOBJECT_DISPLAYID
		//GAMEOBJECT_DISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 Displayid {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_DISPLAYID); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_DISPLAYID, value); }
		}
		#endregion

		#region GAMEOBJECT_FLAGS
		//GAMEOBJECT_FLAGS : type = Shorts, size = 1, flag = Public
		public virtual UInt32 Flags {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_FLAGS); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_FLAGS, value); }
		}
		#endregion

		#region GAMEOBJECT_ROTATION
		//GAMEOBJECT_ROTATION : type = Long, size = 2, flag = Public
		public virtual UInt64 RotationGuid {
			get { return GetUInt64(UpdateFields.GAMEOBJECT_ROTATION); }
			set { SetUInt64(UpdateFields.GAMEOBJECT_ROTATION, value); }
		}
		#endregion

		//GAMEOBJECT_PARENTROTATION : type = Single, size = 4, flag = Public

		#region GAMEOBJECT_POS_X
		//GAMEOBJECT_POS_X : type = Single, size = 1, flag = Public
		public virtual Single PosX {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_X); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_X, value); }
		}
		#endregion

		#region GAMEOBJECT_POS_Y
		//GAMEOBJECT_POS_Y : type = Single, size = 1, flag = Public
		public virtual Single PosY {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_Y); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_Y, value); }
		}
		#endregion

		#region GAMEOBJECT_POS_Z
		//GAMEOBJECT_POS_Z : type = Single, size = 1, flag = Public
		public virtual Single PosZ {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_Z); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_Z, value); }
		}
		#endregion

		#region GAMEOBJECT_FACING
		//GAMEOBJECT_FACING : type = Single, size = 1, flag = Public
		public virtual Single Facing {
			get { return GetSingle(UpdateFields.GAMEOBJECT_FACING); }
			set { SetSingle(UpdateFields.GAMEOBJECT_FACING, value); }
		}
		#endregion

		#region GAMEOBJECT_DYNAMIC
		//GAMEOBJECT_DYNAMIC : type = Shorts, size = 1, flag = Dynamic
		public virtual UInt32 Dynamic {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_DYNAMIC); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_DYNAMIC, value); }
		}
		#endregion

		#region GAMEOBJECT_FACTION
		//GAMEOBJECT_FACTION : type = Int, size = 1, flag = Public
		public virtual UInt32 Faction {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_FACTION); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_FACTION, value); }
		}
		#endregion

		#region GAMEOBJECT_LEVEL
		//GAMEOBJECT_LEVEL : type = Int, size = 1, flag = Public
		public virtual UInt32 Level {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_LEVEL); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_LEVEL, value); }
		}
		#endregion

		#region GAMEOBJECT_BYTES_1
		//GAMEOBJECT_BYTES_1 : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes1 {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_BYTES_1); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_BYTES_1, value); }
		}
		#endregion

	}
}
