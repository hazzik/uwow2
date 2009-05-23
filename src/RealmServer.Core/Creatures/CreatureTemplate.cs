using System;
using System.IO;
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
	}
}