using System;

namespace Hazzik.Objects {
	public partial class GameObject {
		#region OBJECT_FIELD_CREATED_BY

		//OBJECT_FIELD_CREATED_BY : type = Long, size = 2, flag = Public
		public virtual UInt64 CreatedByGuid { get; set; }

		#endregion

		#region GAMEOBJECT_DISPLAYID

		//GAMEOBJECT_DISPLAYID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId { get; set; }

		#endregion

		#region GAMEOBJECT_FLAGS

		//GAMEOBJECT_FLAGS : type = Int, size = 1, flag = Public
		public virtual GameObjectFlags Flags { get; set; }

		#endregion

		public virtual UInt64 Rotation { get; set; }

		#region GAMEOBJECT_PARENTROTATION

		//GAMEOBJECT_PARENTROTATION : type = Single, size = 4, flag = Public
		public virtual Single ParentRotationX { get; set; }

		public virtual Single ParentRotationY { get; set; }

		public virtual Single ParentRotationZ { get; set; }

		public virtual Single ParentRotationO { get; set; }

		#endregion

		#region GAMEOBJECT_DYNAMIC

		//GAMEOBJECT_DYNAMIC : type = Shorts, size = 1, flag = Dynamic
		public virtual GameObjectDynamicFlags DynamicFlags { get; set; }

		public virtual GameObjectDynamicFlagsHigh DynamicFlagsHigh { get; set; }

		#endregion

		#region GAMEOBJECT_FACTION

		//GAMEOBJECT_FACTION : type = Int, size = 1, flag = Public
		public virtual UInt32 Faction { get; set; }

		#endregion

		#region GAMEOBJECT_LEVEL

		//GAMEOBJECT_LEVEL : type = Int, size = 1, flag = Public
		public virtual UInt32 Level { get; set; }

		#endregion

		#region GAMEOBJECT_BYTES_1

		//GAMEOBJECT_BYTES_1 : type = Bytes, size = 1, flag = Public

		public virtual GameObjectState State { get; set; }

		public virtual GameObjectType GameObjectType { get; set; }

		public virtual byte ArtKit { get; set; }

		public virtual byte AnimationProgress { get; set; }

		#endregion
	}
}