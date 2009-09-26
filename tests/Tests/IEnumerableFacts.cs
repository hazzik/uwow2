using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Tests {
	public class IEnumerableFacts {
		private static readonly Assertions _assertions = new Assertions();

		[Fact]
		public void Except() {
			var target = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			var except = new[] { 1, 3, 5, 7, 9 };
			List<int> actual = target.Except(except).ToList();
			_assertions.Equal(5, actual.Count);
			_assertions.Equal(0, actual[0]);
			_assertions.Equal(2, actual[1]);
			_assertions.Equal(4, actual[2]);
			_assertions.Equal(6, actual[3]);
			_assertions.Equal(8, actual[4]);
		}
	}
}