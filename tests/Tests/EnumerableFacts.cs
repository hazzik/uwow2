using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests {
	public class EnumerableFacts : TestFixture {
		[Fact]
		public void Except() {
			var target = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var except = new[] { 1, 3, 5, 7, 9 };
			List<int> actual = target.Except(except).ToList();
			Assert.Equal(5, actual.Count);
			Assert.Equal(0, actual[0]);
			Assert.Equal(2, actual[1]);
			Assert.Equal(4, actual[2]);
			Assert.Equal(6, actual[3]);
			Assert.Equal(8, actual[4]);
		}
	}
}