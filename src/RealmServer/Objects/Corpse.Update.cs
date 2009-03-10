using System;

namespace Hazzik.Objects {
	public partial class Corpse {
		#region CORPSE_FIELD_OWNER
		//CORPSE_FIELD_OWNER : type = Long, size = 2, flag = Public
		public virtual UInt64 OwnerGuid {
			get { return GetUInt64(UpdateFields.CORPSE_FIELD_OWNER); }
			set { SetUInt64(UpdateFields.CORPSE_FIELD_OWNER, value); }
		}
		#endregion

		#region CORPSE_FIELD_PARTY
		//CORPSE_FIELD_PARTY : type = Long, size = 2, flag = Public
		public virtual UInt64 PartyGuid {
			get { return GetUInt64(UpdateFields.CORPSE_FIELD_PARTY); }
			set { SetUInt64(UpdateFields.CORPSE_FIELD_PARTY, value); }
		}
		#endregion

		#region CORPSE_FIELD_FACING
		//CORPSE_FIELD_FACING : type = Single, size = 1, flag = Public
		public override Single Facing {
			get { return GetSingle(UpdateFields.CORPSE_FIELD_FACING); }
			set { SetSingle(UpdateFields.CORPSE_FIELD_FACING, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_X
		//CORPSE_FIELD_POS_X : type = Single, size = 1, flag = Public
		public override Single PosX {
			get { return GetSingle(UpdateFields.CORPSE_FIELD_POS_X); }
			set { SetSingle(UpdateFields.CORPSE_FIELD_POS_X, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_Y
		//CORPSE_FIELD_POS_Y : type = Single, size = 1, flag = Public
		public override Single PosY {
			get { return GetSingle(UpdateFields.CORPSE_FIELD_POS_Y); }
			set { SetSingle(UpdateFields.CORPSE_FIELD_POS_Y, value); }
		}
		#endregion

		#region CORPSE_FIELD_POS_Z
		//CORPSE_FIELD_POS_Z : type = Single, size = 1, flag = Public
		public override Single PosZ {
			get { return GetSingle(UpdateFields.CORPSE_FIELD_POS_Z); }
			set { SetSingle(UpdateFields.CORPSE_FIELD_POS_Z, value); }
		}
		#endregion

		#region CORPSE_FIELD_DISPLAY_ID
		//CORPSE_FIELD_DISPLAY_ID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId {
			get { return GetUInt32(UpdateFields.CORPSE_FIELD_DISPLAY_ID); }
			set { SetUInt32(UpdateFields.CORPSE_FIELD_DISPLAY_ID, value); }
		}
		#endregion

		//CORPSE_FIELD_ITEM : type = Int, size = 19, flag = Public

		#region CORPSE_FIELD_BYTES_1
		//CORPSE_FIELD_BYTES_1 : type = Bytes, size = 1, flag = Public
		public virtual UInt32 Bytes1 {
			get { return GetUInt32(UpdateFields.CORPSE_FIELD_BYTES_1); }
			set { SetUInt32(UpdateFields.CORPSE_FIELD_BYTES_1, value); }
		}
		#endregion

		#region CORPSE_FIELD_BYTES_2
		//CORPSE_FIELD_BYTES_2 : type = Bytes, size = 1, flag = Public
		public byte Face {
			get { return GetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 0); }
			set { SetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 0, value); }
		}

		public byte HairStyle {
			get { return GetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 1); }
			set { SetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 1, value); }
		}

		public byte HairColor {
			get { return GetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 2); }
			set { SetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 2, value); }
		}

		public byte FacialHair {
			get { return GetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 3); }
			set { SetByte(UpdateFields.CORPSE_FIELD_BYTES_2, 3, value); }
		}
		#endregion

		#region CORPSE_FIELD_GUILD
		//CORPSE_FIELD_GUILD : type = Int, size = 1, flag = Public
		public virtual UInt32 GuildId {
			get { return GetUInt32(UpdateFields.CORPSE_FIELD_GUILD); }
			set { SetUInt32(UpdateFields.CORPSE_FIELD_GUILD, value); }
		}
		#endregion

		#region CORPSE_FIELD_FLAGS
		//CORPSE_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual UInt32 Flags {
			get { return GetUInt32(UpdateFields.CORPSE_FIELD_FLAGS); }
			set { SetUInt32(UpdateFields.CORPSE_FIELD_FLAGS, value); }
		}
		#endregion

		#region CORPSE_FIELD_DYNAMIC_FLAGS
		//CORPSE_FIELD_DYNAMIC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual UInt32 DynamicFlags {
			get { return GetUInt32(UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS); }
			set { SetUInt32(UpdateFields.CORPSE_FIELD_DYNAMIC_FLAGS, value); }
		}
		#endregion

		//CORPSE_FIELD_PAD : type = Int, size = 1, flag = None
	}
}
