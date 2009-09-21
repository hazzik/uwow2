using System;

namespace Hazzik.Gossip {
	public class NpcTexts {
		public NpcTexts() {
		}

		public NpcTexts(uint id, NpcText[] texts) {
			Id = id;
			Texts = texts;
		}

		public NpcText[] Texts { get; private set; }

		public uint Id { get; private set; }
	}
}