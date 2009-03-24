using System;
using System.Collections.Generic;
using Hazzik;
using Hazzik.Map;
using Hazzik.Objects;
using Xunit;

namespace Tests {
	public class PlayerFacts {
		private class TestPlayer:Player {
			public new ICollection<IUpdateBlock> GetUpdateBuilders() {
				return base.GetUpdateBuilders();
			}
		}

		[Fact]
		public void GetUpdateBuilders() {
			var player = CreatePlayer();
			var builders = player.GetUpdateBuilders();
			Assert.Equal(0, builders.Count);
		}

		[Fact]
		public void GetUpdateBuilders2() {
			var player = CreatePlayer();
			ObjectManager.Add(player);
			var builders = player.GetUpdateBuilders();
			Assert.Equal(1, builders.Count);
			ObjectManager.Remove(player);
		}

		[Fact]
		public void GetUpdateBuilders3() {
			var player = CreatePlayer();
			ObjectManager.Add(player);
			var builders = player.GetUpdateBuilders();
			Assert.Equal(1, builders.Count);
			var builders2 = player.GetUpdateBuilders();
			Assert.Equal(0, builders2.Count);
			ObjectManager.Remove(player);
		}
		[Fact]
		public void GetUpdateBuilders4() {
			var player = CreatePlayer();
			ObjectManager.Add(player);
			var builders = player.GetUpdateBuilders();
			Assert.Equal(1, builders.Count);
			var builders2 = player.GetUpdateBuilders();
			Assert.Equal(0, builders2.Count);
			player.Health = 0;
			var builders3 = player.GetUpdateBuilders();
			Assert.Equal(1, builders3.Count);
			ObjectManager.Remove(player);
		}

		private static TestPlayer CreatePlayer() {
			return new TestPlayer();
		}
	}
}
