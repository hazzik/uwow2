using System;

namespace Hazzik {
	public class WorldServerInfo {
		public byte Type { get; set; }
		public byte Locked { get; set; }
		public byte Status { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public float Population { get; set; }
		public byte CharactersCount { get; set; }
		public byte Language { get; set; }
		public byte Unk { get; set; }
	}
}