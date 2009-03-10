using System;

namespace Hazzik.Objects {
	public partial class Corpse : Positioned {
		public Corpse()
			: this((int)UpdateFields.CORPSE_END) {
		}

		protected Corpse(int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Corpse;
		}

		public override byte TypeId {
			get { return (byte)ObjectTypeId.Corpse; }
		}

		public static Corpse Create(Player player) {
			var corpse = new Corpse {
				OwnerGuid = player.Guid,
				//PartyGuid = ,
				Facing = player.Facing,
				PosX = player.PosX,
				PosY = player.PosY,
				PosZ = player.PosZ,
				DisplayId = player.DisplayId,
				//Bytes1_0 = , // classe?
				Race = player.Race,
				Gender = player.Gender,
				Skin = player.Skin,
				Face = player.Face,
				HairStyle = player.HairStyle,
				HairColor = player.HairColor,
				FacialHair = player.FacialHair,
				GuildId = player.GuildId,
				//Flags = CorpseFlags.Bones,
				DynamicFlags = CorpseDynamicFlags.None,
			};
			return corpse;
		}
	}
}