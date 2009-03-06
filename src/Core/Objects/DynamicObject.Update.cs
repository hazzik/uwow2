using System;

namespace Hazzik.Objects {
	public partial class DynamicObject {
		#region DYNAMICOBJECT_CASTER
		//DYNAMICOBJECT_CASTER : type = Long, size = 2, flag = Public
		public virtual UInt64 CasterGuid {
			get { return GetValueUInt64((int)UpdateFields.DYNAMICOBJECT_CASTER); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_CASTER, value); }
		}
		#endregion

		#region DYNAMICOBJECT_BYTES
		//DYNAMICOBJECT_BYTES : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes {
			get { return GetValueUInt32((int)UpdateFields.DYNAMICOBJECT_BYTES); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_BYTES, value); }
		}
		#endregion

		#region DYNAMICOBJECT_SPELLID
		//DYNAMICOBJECT_SPELLID : type = Int, size = 1, flag = Public
		public virtual UInt32 Spellid {
			get { return GetValueUInt32((int)UpdateFields.DYNAMICOBJECT_SPELLID); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_SPELLID, value); }
		}
		#endregion

		#region DYNAMICOBJECT_RADIUS
		//DYNAMICOBJECT_RADIUS : type = Single, size = 1, flag = Public
		public virtual Single Radius {
			get { return GetValueSingle((int)UpdateFields.DYNAMICOBJECT_RADIUS); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_RADIUS, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_X
		//DYNAMICOBJECT_POS_X : type = Single, size = 1, flag = Public
		public virtual Single PosX {
			get { return GetValueSingle((int)UpdateFields.DYNAMICOBJECT_POS_X); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_POS_X, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_Y
		//DYNAMICOBJECT_POS_Y : type = Single, size = 1, flag = Public
		public virtual Single PosY {
			get { return GetValueSingle((int)UpdateFields.DYNAMICOBJECT_POS_Y); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_POS_Y, value); }
		}
		#endregion

		#region DYNAMICOBJECT_POS_Z
		//DYNAMICOBJECT_POS_Z : type = Single, size = 1, flag = Public
		public virtual Single PosZ {
			get { return GetValueSingle((int)UpdateFields.DYNAMICOBJECT_POS_Z); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_POS_Z, value); }
		}
		#endregion

		#region DYNAMICOBJECT_FACING
		//DYNAMICOBJECT_FACING : type = Single, size = 1, flag = Public
		public virtual Single Facing {
			get { return GetValueSingle((int)UpdateFields.DYNAMICOBJECT_FACING); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_FACING, value); }
		}
		#endregion

		#region DYNAMICOBJECT_CASTTIME
		//DYNAMICOBJECT_CASTTIME : type = Int, size = 1, flag = Public
		public virtual UInt32 Casttime {
			get { return GetValueUInt32((int)UpdateFields.DYNAMICOBJECT_CASTTIME); }
			set { SetValue((int)UpdateFields.DYNAMICOBJECT_CASTTIME, value); }
		}
		#endregion
	}
}
