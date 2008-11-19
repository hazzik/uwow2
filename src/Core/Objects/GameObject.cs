using System;

namespace Hazzik.Objects {
	public class GameObject : Positioned {
		public GameObject()
			: this((int)UpdateFields.GAMEOBJECT_END, 0x21) {
		}

		protected GameObject(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.GameObject; }
		}

		public override byte UpdateFlag {
			get { return (byte)(UpdateFlags.LowGuid | UpdateFlags.HighGuid); }
		}

		#region UpdateFields

		//OBJECT_FIELD_CREATED_BY = OBJECT_END + 0, // 2 4 1
		public long CreatedByGuid { get; set; }
		//GAMEOBJECT_DISPLAYID = OBJECT_END + 2, // 1 1 1
		public int DisplayID { get; set; }
		//GAMEOBJECT_FLAGS = OBJECT_END + 3, // 1 1 1
		public int Flags { get; set; }
		//GAMEOBJECT_ROTATION = OBJECT_END + 4, // 2 4 1
		//GAMEOBJECT_PARENTROTATION = OBJECT_END + 6, // 4 3 1
		//GAMEOBJECT_POS_X = OBJECT_END + 10, // 1 3 1
		public float PosX { get; set; }
		//GAMEOBJECT_POS_Y = OBJECT_END + 11, // 1 3 1
		public float PosY { get; set; }
		//GAMEOBJECT_POS_Z = OBJECT_END + 12, // 1 3 1
		public float PosZ { get; set; }
		//GAMEOBJECT_FACING = OBJECT_END + 13, // 1 3 1
		public float Facing { get; set; }
		//GAMEOBJECT_DYN_FLAGS = OBJECT_END + 14, // 1 1 256
		public int DynFlags { get; set; }
		//GAMEOBJECT_FACTION = OBJECT_END + 15, // 1 1 1
		public int Faction { get; set; }
		//GAMEOBJECT_LEVEL = OBJECT_END + 16, // 1 1 1
		public int Level { get; set; }
		//GAMEOBJECT_BYTES_1 = OBJECT_END + 17, // 1 5 1
		public int Bytes1 { get; set; }
		//GAMEOBJECT_END = OBJECT_END + 18,

		#endregion
	}
}