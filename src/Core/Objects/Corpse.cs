using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Objects {
	public class Corpse : Positioned {
		public Corpse()
			: this((int)UpdateFields.CORPSE_END, 0x81) {
		}

		protected Corpse(int updateMaskLength, uint type)
			: base(updateMaskLength, type) {
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Corpse; }
		}

		#region UpdateFields
		
		//CORPSE_FIELD_OWNER = OBJECT_END + 0, // 2 4 1
		public long OwnerGuid { get; set; }
		//CORPSE_FIELD_PARTY = OBJECT_END + 2, // 2 4 1
		public long PartyGuid { get; set; }
		//CORPSE_FIELD_FACING = OBJECT_END + 4, // 1 3 1
		public float Facing { get; set; }
		//CORPSE_FIELD_POS_X = OBJECT_END + 5, // 1 3 1
		public float PosX { get; set; }
		//CORPSE_FIELD_POS_Y = OBJECT_END + 6, // 1 3 1
		public float PosY { get; set; }
		//CORPSE_FIELD_POS_Z = OBJECT_END + 7, // 1 3 1
		public float PosZ { get; set; }
		//CORPSE_FIELD_DISPLAY_ID = OBJECT_END + 8, // 1 1 1
		public int DisplayID { get; set; }
		//CORPSE_FIELD_ITEM = OBJECT_END + 9, // 19 1 1
		//CORPSE_FIELD_BYTES_1 = OBJECT_END + 28, // 1 5 1
		public int Bytes1 { get; set; }
		//CORPSE_FIELD_BYTES_2 = OBJECT_END + 29, // 1 5 1
		public int Bytes2 { get; set; }
		//CORPSE_FIELD_GUILD = OBJECT_END + 30, // 1 1 1
		public int Guild { get; set; }
		//CORPSE_FIELD_FLAGS = OBJECT_END + 31, // 1 1 1
		public int Flags { get; set; }
		//CORPSE_FIELD_DYNAMIC_FLAGS = OBJECT_END + 32, // 1 1 256
		public int DynamicFlags { get; set; }
		//CORPSE_FIELD_PAD = OBJECT_END + 33, // 1 1 0
		public int Pad { get { return 0; } }
		//CORPSE_END = OBJECT_END + 34,

		#endregion
	}
}
