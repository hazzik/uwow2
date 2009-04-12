using Hazzik.Net;

namespace Hazzik.Creatures {
	public class CreatureTemplate {
		public uint Id { get; set; }
		
		public string Name { get; set; }
		
		public string GuildName { get; set; }
		
		public CreatureFlags Flags { get; set; }

		public CreatureType Type { get; set; }

		public CreatureFamily Family { get; set; }

		public CreatureRank Rank { get; set; }

		public uint DisplayId { get; set; }

		public IPacket GetResponce() {
			var packet = new WorldPacket(WMSG.SMSG_CREATURE_QUERY_RESPONSE);
			var writer = packet.CreateWriter();
			writer.Write(Id);
			writer.WriteCString(Name);
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString(GuildName);
			writer.Write((uint)Flags);
			writer.Write((uint)Type);
			writer.Write((uint)Family);
			writer.Write((uint)Rank);
			writer.Write(0);
			writer.Write(0); // SpellGroupId
			writer.Write(DisplayId);
			writer.Write(0);
			writer.Write(0);
			writer.Write(1f);
			writer.Write(1f);
			writer.Write((byte)0);
			return packet;
		}
	}
}