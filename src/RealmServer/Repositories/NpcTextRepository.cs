using System;
using Hazzik.Gossip;

namespace Hazzik.Repositories {
	public class NpcTextRepository {
		public static NpcTexts FindById(uint textId) {
			return new NpcTexts(textId, new[] {
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 = "Hello sir?", Text1 = "Hello sir?" },
			});
		}
	}
}