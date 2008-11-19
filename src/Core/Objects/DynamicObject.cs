using System;

namespace Hazzik.Objects {
	public class DynamicObject : Positioned {
		public DynamicObject()
			: this((int)UpdateFields.DYNAMICOBJECT_END, 0x41) {
		}

		protected DynamicObject(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.DynamicObject; }
		}

		public override byte UpdateFlag {
			get { return (byte)(UpdateFlags.LowGuid | UpdateFlags.HighGuid); }
		}

		#region UpdateFields

		//DYNAMICOBJECT_CASTER = OBJECT_END + 0, // 2 4 1
		public long CasterGuid { get; set; }
		//DYNAMICOBJECT_BYTES = OBJECT_END + 2, // 1 5 1
		public int Bytes { get; set; }
		//DYNAMICOBJECT_SPELLID = OBJECT_END + 3, // 1 1 1
		public int SpellID { get; set; }
		//DYNAMICOBJECT_RADIUS = OBJECT_END + 4, // 1 3 1
		public float Radius { get; set; }
		//DYNAMICOBJECT_POS_X = OBJECT_END + 5, // 1 3 1
		public float PosX { get; set; }
		//DYNAMICOBJECT_POS_Y = OBJECT_END + 6, // 1 3 1
		public float PosY { get; set; }
		//DYNAMICOBJECT_POS_Z = OBJECT_END + 7, // 1 3 1
		public float PosZ { get; set; }
		//DYNAMICOBJECT_FACING = OBJECT_END + 8, // 1 3 1
		public float Facing { get; set; }
		//DYNAMICOBJECT_CASTTIME = OBJECT_END + 9, // 1 1 1
		public int CastTime { get; set; }
		//DYNAMICOBJECT_END = OBJECT_END + 10, 

		#endregion
	}
}