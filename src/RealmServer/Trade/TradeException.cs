using System;

namespace Hazzik.Trade {
	public class TradeException : Exception {
		public TradeException(TradeStatus status) {
			Code = status;
		}

		public TradeStatus Code { get; private set; }
	}
}