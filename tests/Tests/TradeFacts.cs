using System;
using Hazzik.Objects;
using Hazzik.Trade;
using Xunit;

namespace Tests {
	public class TradeFacts:TestFixture {
		[Fact]
		public void ConstructorAcceptsPlayer() {
			var player = new Player();
			var context = new TradeContext(player);
			Assert.Equal(player, context.Contractor);
		}

		[Fact]
		public void BegintradeWithNullPlayerThrownTradeException() {
			var player = new Player();
			var context = new TradeContext(player);
			var exception = Xunit.Assert.Throws<TradeException>(() => context.BeginTrade(null));
			Assert.Equal(TradeStatus.PlayerNotFound, exception.Code);
		}
	}
}