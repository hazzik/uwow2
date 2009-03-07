using System;

namespace Hazzik.Objects {
	public partial class DynamicObject {
		#region DYNAMICOBJECT_CASTER
		//DYNAMICOBJECT_CASTER : type = Long, size = 2, flag = Public
		public virtual UInt64 CasterGuid {
			get { return GetUInt64(UpdateFields.DYNAMICOBJECT_CASTER); }
			set { SetUInt64(UpdateFields.DYNAMICOBJECT_CASTER, value); }
		}
		#endregion

		#region DYNAMICOBJECT_BYTES
		//DYNAMICOBJECT_BYTES : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes {
			get { return GetUInt32(UpdateFields.DYNAMICOBJECT_BYTES); }
			set { SetUInt32(UpdateFields.DYNAMICOBJECT_BYTES, value); }
		}
		#endregion

		#region DYNAMICOBJECT_SPELLID
		//DYNAMICOBJECT_SPELLID : type = Int, size = 1, flag = Public
		public virtual UInt32 Spellid {
			get { return GetUInt32(UpdateFields.DYNAMICOBJECT_SPELLID); }
			set { SetUInt32(UpdateFields.DYNAMICOBJECT_SPELLID, value); }
		}
		#endregion

		#region DYNAMICOBJECT_RADIUS
		//DYNAMICOBJECT_RADIUS : type = Single, size = 1, flag = Public
		public virtual Single Radius {
			get { return GetSingle(UpdateFields.DYNAMICOBJECT_RADIUS); }
			set { SetSingle(UpdateFields.DYNAMICOBJECT_RADIUS, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_X
		//DYNAMICOBJECT_POS_X : type = Single, size = 1, flag = Public
		public virtual Single PosX {
			get { return GetSingle(UpdateFields.DYNAMICOBJECT_POS_X); }
			set { SetSingle(UpdateFields.DYNAMICOBJECT_POS_X, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_Y
		//DYNAMICOBJECT_POS_Y : type = Single, size = 1, flag = Public
		public virtual Single PosY {
			get { return GetSingle(UpdateFields.DYNAMICOBJECT_POS_Y); }
			set { SetSingle(UpdateFields.DYNAMICOBJECT_POS_Y, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_Z
		//DYNAMICOBJECT_POS_Z : type = Single, size = 1, flag = Public
		public virtual Single PosZ {
			get { return GetSingle(UpdateFields.DYNAMICOBJECT_POS_Z); }
			set { SetSingle(UpdateFields.DYNAMICOBJECT_POS_Z, value); }
		}
		#endregion

		#region DYNAMICOBJECT_FACING
		//DYNAMICOBJECT_FACING : type = Single, size = 1, flag = Public
		public virtual Single Facing {
			get { return GetSingle(UpdateFields.DYNAMICOBJECT_FACING); }
			set { SetSingle(UpdateFields.DYNAMICOBJECT_FACING, value); }
		}
		#endregion

		#region DYNAMICOBJECT_CASTTIME
		//DYNAMICOBJECT_CASTTIME : type = Int, size = 1, flag = Public
		public virtual UInt32 Casttime {
			get { return GetUInt32(UpdateFields.DYNAMICOBJECT_CASTTIME); }
			set { SetUInt32(UpdateFields.DYNAMICOBJECT_CASTTIME, value); }
		}
		#endregion
	}
}
