using System;

namespace Hazzik.Objects {
	public partial class Corpse {
		#region CORPSE_FIELD_OWNER

		//CORPSE_FIELD_OWNER : type = Long, size = 2, flag = Public
		public virtual UInt64 OwnerGuid { get; set; }

		#endregion

		#region CORPSE_FIELD_PARTY

		//CORPSE_FIELD_PARTY : type = Long, size = 2, flag = Public
		public virtual UInt64 PartyGuid { get; set; }

		#endregion

		#region CORPSE_FIELD_DISPLAY_ID

		//CORPSE_FIELD_DISPLAY_ID : type = Int, size = 1, flag = Public
		public virtual UInt32 DisplayId { get; set; }

		#endregion

		#region CORPSE_FIELD_BYTES_1

		//CORPSE_FIELD_BYTES_1 : type = Bytes, size = 1, flag = Public
		public virtual byte Bytes1_0 { get; set; }

		public virtual Races Race { get; set; }

		public virtual GenderType Gender { get; set; }

		public virtual byte Skin { get; set; }

		#endregion

		#region CORPSE_FIELD_BYTES_2

		//CORPSE_FIELD_BYTES_2 : type = Bytes, size = 1, flag = Public
		public virtual byte Face { get; set; }

		public virtual byte HairStyle { get; set; }

		public virtual byte HairColor { get; set; }

		public virtual byte FacialHair { get; set; }

		#endregion

		#region CORPSE_FIELD_GUILD

		//CORPSE_FIELD_GUILD : type = Int, size = 1, flag = Public
		public virtual Int32 GuildId { get; set; }

		#endregion

		#region CORPSE_FIELD_FLAGS

		//CORPSE_FIELD_FLAGS : type = Int, size = 1, flag = Public
		public virtual CorpseFlags Flags { get; set; }

		#endregion

		#region CORPSE_FIELD_DYNAMIC_FLAGS

		//CORPSE_FIELD_DYNAMIC_FLAGS : type = Int, size = 1, flag = Dynamic
		public virtual CorpseDynamicFlags DynamicFlags { get; set; }

		#endregion

		//CORPSE_FIELD_ITEM : type = Int, size = 19, flag = Public

		//CORPSE_FIELD_PAD : type = Int, size = 1, flag = None
	}
}