using System;

namespace Hazzik.Objects {
	public partial class Corpse {
		#region CORPSE_FIELD_OWNER
		//CORPSE_FIELD_OWNER : type = Long, size = 2, flag = Public
		public virtual UInt64 OwnerGuid {
			get { return GetValueUInt64((int)UpdateFields.CORPSE_FIELD_OWNER); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_OWNER, value); }
		}
		#endregion

		#region CORPSE_FIELD_PARTY
		//CORPSE_FIELD_PARTY : type = Long, size = 2, flag = Public
		public virtual UInt64 PartyGuid {
			get { return GetValueUInt64((int)UpdateFields.CORPSE_FIELD_PARTY); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_PARTY, value); }
		}
		#endregion

		#region CORPSE_FIELD_FACING
		//CORPSE_FIELD_FACING : type = Single, size = 1, flag = Public
		public virtual Single Facing {
			get { return GetValueSingle((int)UpdateFields.CORPSE_FIELD_FACING); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_FACING, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_X
		//CORPSE_FIELD_POS_X : type = Single, size = 1, flag = Public
		public virtual Single PosX {
			get { return GetValueSingle((int)UpdateFields.CORPSE_FIELD_POS_X); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_POS_X, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_Y
		//CORPSE_FIELD_POS_Y : type = Single, size = 1, flag = Public
		public virtual Single PosY {
			get { return GetValueSingle((int)UpdateFields.CORPSE_FIELD_POS_Y); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_POS_Y, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_Z
		//CORPSE_FIELD_POS_Z : type = Single, size = 1, flag = Public
		public virtual Single PosZ {
			get { return GetValueSingle((int)UpdateFields.CORPSE_FIELD_POS_Z); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_POS_Z, value); }
		}
		#endregion

		#region CORPSE_FIELD_DISPLAY_ID
		//CORPSE_FIELD_DISPLAY_ID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_DISPLAY_ID); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_DISPLAY_ID, value); }
		}
		#endregion

		//CORPSE_FIELD_ITEM : type = Int, size = 19, flag = Public

		#region CORPSE_FIELD_BYTES_1
		//CORPSE_FIELD_BYTES_1 : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes1 {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_BYTES_1); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_BYTES_1, value); }
		}
		#endregion

		#region CORPSE_FIELD_BYTES_2
		//CORPSE_FIELD_BYTES_2 : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes2 {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_BYTES_2); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_BYTES_2, value); }
		}
		#endregion

		#region CORPSE_FIELD_GUILD
		//CORPSE_FIELD_GUILD : type = Int, size = 1, flag = Public
		public virtual UInt32 Guild {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_GUILD); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_GUILD, value); }
		}
		#endregion

		#region CORPSE_FIELD_FLAGS
		//CORPSE_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_FLAGS); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_FLAGS, value); }
		}
		#endregion

		#region CORPSE_FIELD_DYNAMIC_FLAGS
		//CORPSE_FIELD_DYNAMIC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual UInt32 DynamicFlags {
			get { return GetValueUInt32((int)UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS); }
			set { SetValue((int)UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS, value); }
		}
		#endregion

		//CORPSE_FIELD_PAD : type = Int, size = 1, flag = None
	}
}
