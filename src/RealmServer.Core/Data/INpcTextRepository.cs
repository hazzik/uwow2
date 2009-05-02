using System;
using Hazzik.Gossip;

namespace Hazzik.Data {
	public interface INpcTextRepository {
		NpcTexts FindById(uint textId);
	}
}