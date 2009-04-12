using System;

namespace Hazzik.Gossip {
	public class NpcText {
		public int Id { get; set; }
		public float Probability { get; set; }
		public string Text0 { get; set; }
		public string Text1 { get; set; }
		public int Language { get; set; }
		public int[,] Emote { get; set; }
	}
}