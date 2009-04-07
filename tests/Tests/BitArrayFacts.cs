using System;
using System.Collections;
using Xunit;

namespace Tests {
	public class BitArrayFacts {
		[Fact]
		public void Length() {
			var ba = new BitArray(32);
			ba.SetAll(true);
			ba.Length = 16;
			Assert.Equal(16, ba.Length);
		}
	}
}
