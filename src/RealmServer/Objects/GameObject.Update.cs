using System;

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
		public virtual UInt32 DisplayId {
			get { return GetUInt32(UpdateFields.GAMEOBJECT_DISPLAYID); }
			set { SetUInt32(UpdateFields.GAMEOBJECT_DISPLAYID, value); }
		}
		#endregion

		#region GAMEOBJECT_FLAGS
		//GAMEOBJECT_FLAGS : type = Shorts, size = 1, flag = Public
		public virtual GameObjectFlags Flags {
			get { return (GameObjectFlags)GetUInt16(UpdateFields.GAMEOBJECT_FLAGS, 0); }
			set { SetUInt16(UpdateFields.GAMEOBJECT_FLAGS, 0, (ushort)value); }
		}

		public virtual GameObjectFlagsHigh FlagsHigh {
			get { return (GameObjectFlagsHigh)GetUInt16(UpdateFields.GAMEOBJECT_FLAGS, 1); }
			set { SetUInt16(UpdateFields.GAMEOBJECT_FLAGS, 1, (ushort)value); }
		}
		#endregion

		#region GAMEOBJECT_ROTATION
		//GAMEOBJECT_ROTATION : type = Long, size = 2, flag = Public
		public virtual UInt64 Rotation {
			get { return GetUInt64(UpdateFields.GAMEOBJECT_ROTATION); }
			set { SetUInt64(UpdateFields.GAMEOBJECT_ROTATION, value); }
		}
		#endregion

		#region GAMEOBJECT_PARENTROTATION
		//GAMEOBJECT_PARENTROTATION : type = Single, size = 4, flag = Public
		public virtual Single ParentRotationX {
			get { return GetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION); }
			set { SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION, value); }
		}
		
		public virtual Single ParentRotationY {
			get { return GetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 1); }
			set { SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 1, value); }
		}
		
		public virtual Single ParentRotationZ {
			get { return GetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 2); }
			set { SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 2, value); }
		}
		
		public virtual Single ParentRotationO {
			get { return GetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 3); }
			set { SetSingle(UpdateFields.GAMEOBJECT_PARENTROTATION + 3, value); }
		}
		#endregion

		#region GAMEOBJECT_POS_X
		//GAMEOBJECT_POS_X : type = Single, size = 1, flag = Public
		public override Single PosX {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_X); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_X, value); }
		}
		#endregion

		#region GAMEOBJECT_POS_Y
		//GAMEOBJECT_POS_Y : type = Single, size = 1, flag = Public
		public override Single PosY {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_Y); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_Y, value); }
		}
		#endregion

		#region GAMEOBJECT_POS_Z
		//GAMEOBJECT_POS_Z : type = Single, size = 1, flag = Public
		public override Single PosZ {
			get { return GetSingle(UpdateFields.GAMEOBJECT_POS_Z); }
			set { SetSingle(UpdateFields.GAMEOBJECT_POS_Z, value); }
		}
		#endregion

		#region GAMEOBJECT_FACING
		//GAMEOBJECT_FACING : type = Single, size = 1, flag = Public
		public override Single Facing {
			get { return GetSingle(UpdateFields.GAMEOBJECT_FACING); }
			set { SetSingle(UpdateFields.GAMEOBJECT_FACING, value); }
		}
		#endregion

		#region GAMEOBJECT_DYNAMIC
		//GAMEOBJECT_DYNAMIC : type = Shorts, size = 1, flag = Dynamic
		public virtual GameObjectDynamicFlags DynamicFlags {
			get { return (GameObjectDynamicFlags)GetUInt16(UpdateFields.GAMEOBJECT_DYNAMIC, 0); }
			set { SetUInt16(UpdateFields.GAMEOBJECT_DYNAMIC,0, (ushort)value); }
		}

		public virtual GameObjectDynamicFlagsHigh DynamicFlagsHigh {
			get { return (GameObjectDynamicFlagsHigh)GetUInt16(UpdateFields.GAMEOBJECT_DYNAMIC, 1); }
			set { SetUInt16(UpdateFields.GAMEOBJECT_DYNAMIC, 1, (ushort)value); }
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

		public GameObjectState State {
			get { return (GameObjectState)GetByte(UpdateFields.GAMEOBJECT_BYTES_1, 0); }
			set { SetByte(UpdateFields.GAMEOBJECT_BYTES_1, 0, (byte)value); }
		}

		public GameObjectType GameObjectType {
			get { return (GameObjectType)GetByte(UpdateFields.GAMEOBJECT_BYTES_1, 1); }
			set { SetByte(UpdateFields.GAMEOBJECT_BYTES_1, 1, (byte)value); }
		}

		public byte ArtKit {
			get { return GetByte(UpdateFields.GAMEOBJECT_BYTES_1, 2); }
			set { SetByte(UpdateFields.GAMEOBJECT_BYTES_1, 2, value); }
		}

		public byte AnimationProgress {
			get { return GetByte(UpdateFields.GAMEOBJECT_BYTES_1, 3); }
			set { SetByte(UpdateFields.GAMEOBJECT_BYTES_1, 3, value); }
		}

		#endregion

	}
}
