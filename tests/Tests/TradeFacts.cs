using Hazzik.Objects;
using Hazzik.Trade;
using Xunit;
using Xunit.Extensions;

namespace Tests {
	public class TradeFacts {
		public static Assertions Assert = new Assertions();
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
			var exception = Assert.Throws<TradeException>(() => context.BeginTrade(null));
			Assert.Equal(TradeStatus.PlayerNotFound, exception.Code);
		}
	}
}