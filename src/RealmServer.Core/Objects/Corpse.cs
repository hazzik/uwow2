using System;

namespace Hazzik.Objects {
	public partial class Corpse : Positioned {
		public Corpse() {
			Type |= ObjectTypes.Corpse;
		}

		public Player Owner { get; private set; }

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Corpse; }
		}

		public static Corpse Create(Player player) {
			var corpse = new Corpse {
				Owner = player,
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