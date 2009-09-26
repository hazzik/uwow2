using System;
using Hazzik.Gossip;

namespace Hazzik.Data.Fake {
	public class FakeNpcTextRepository : INpcTextRepository {
		#region INpcTextRepository Members

		public NpcTexts FindById(uint textId) {
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

		#endregion
	}
}