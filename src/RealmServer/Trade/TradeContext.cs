using System;
using Hazzik.Objects;

namespace Hazzik.Trade {
	public class TradeContext {
		public TradeContext(Player player) {
			Contractor = player;
		}

		public Player Contractor { get; private set; }

		public void BeginTrade(Player other) {
			if(other == null) {
				throw new TradeException(TradeStatus.PlayerNotFound);
			}
		}
	}
}