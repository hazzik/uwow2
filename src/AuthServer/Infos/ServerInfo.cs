using System;

namespace Hazzik {
	public enum ServerType : byte {
		Normal = 0,
		PvP = 1,
		RP = 6,
		PvPRP = 8,
	}

	public enum ServerStatus : byte {
		Green = 0,
		Red = 1,
		Offline = 2,

		RecomendedBlue = 32,
		RecomendedGreen = 64,
		Full = 128,
	}

	public class WorldServerInfo {
		public ServerType Type { get; set; }
		public bool Locked { get; set; }
		public ServerStatus Status { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public float Population { get; set; }
		public byte CharactersCount { get; set; }
		public byte Language { get; set; }
		public byte Unk { get; set; }
	}
}