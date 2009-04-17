using System;

namespace Hazzik.Objects {
	public partial class DynamicObject {
		#region DYNAMICOBJECT_CASTER

		//DYNAMICOBJECT_CASTER : type = Long, size = 2, flag = Public
		public virtual UInt64 CasterGuid { get; set; }

		#endregion

		#region DYNAMICOBJECT_BYTES

		//DYNAMICOBJECT_BYTES : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes { get; set; }

		#endregion

		#region DYNAMICOBJECT_SPELLID

		//DYNAMICOBJECT_SPELLID : type = Int, size = 1, flag = Public
		public virtual UInt32 SpellId { get; set; }

		#endregion

		#region DYNAMICOBJECT_RADIUS

		//DYNAMICOBJECT_RADIUS : type = Single, size = 1, flag = Public
		public virtual Single Radius { get; set; }

		#endregion

		#region DYNAMICOBJECT_POS_X

		//DYNAMICOBJECT_POS_X : type = Single, size = 1, flag = Public
		public override Single PosX { get; set; }

		#endregion

		#region DYNAMICOBJECT_POS_Y

		//DYNAMICOBJECT_POS_Y : type = Single, size = 1, flag = Public
		public override Single PosY { get; set; }

		#endregion

		#region DYNAMICOBJECT_POS_Z

		//DYNAMICOBJECT_POS_Z : type = Single, size = 1, flag = Public
		public override Single PosZ { get; set; }

		#endregion

		#region DYNAMICOBJECT_FACING

		//DYNAMICOBJECT_FACING : type = Single, size = 1, flag = Public
		public override Single Facing { get; set; }

		#endregion

		#region DYNAMICOBJECT_CASTTIME

		//DYNAMICOBJECT_CASTTIME : type = Int, size = 1, flag = Public
		public virtual UInt32 CastTime { get; set; }

		#endregion
	}
}