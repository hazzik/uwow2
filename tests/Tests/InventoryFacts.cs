using System;
using System.Linq;
using Hazzik.Objects;
using Xunit;

namespace Tests {
	public class InventoryFacts {
		private static TestInventory CreateInventory() {
			return CreateInventory(new Player());
		}

		private static TestInventory CreateInventory(IContainer player) {
			return CreateInventory(player, 100);
		}

		private static TestInventory CreateInventory(uint slotsCount) {
			return CreateInventory(new Player(), slotsCount);
		}

		private static TestInventory CreateInventory(IContainer player, uint slotsCount) {
			return new TestInventory(player, slotsCount);
		}
		
		private static Item CreateItem() {
			return ItemFactory.Create(new ItemTemplate());
		}

		private static Container CreateContainer(int slots) {
			return (Container)ItemFactory.Create(new ItemTemplate { ContainerSlots = slots });
		}

		[Fact]
		public void Ctor() {
			var player = new Player();
			IInventory inventory = CreateInventory(player, 100);
			Assert.Equal(player, inventory.Container.Owner);
			Assert.Equal(100u, inventory.MaxCount);
		}

		[Fact]
		public void SetItem() {
			TestInventory inventory = CreateInventory();
			var item = new Item(new ItemTemplate());
			inventory.SetItem(1, item);
			Assert.Equal(item, inventory.Items[1]);
		}

		[Fact]
		public void SetItemOnNegativeSlotThrowException() {
			TestInventory inventory = CreateInventory();
			Assert.Throws<ArgumentOutOfRangeException>(() => inventory.SetItem(-1, null));
		}

		[Fact]
		public void SetItemOnSlotGraterThenSlotsCountThrowException() {
			TestInventory inventory = CreateInventory(99);
			Assert.Throws<ArgumentOutOfRangeException>(() => inventory.SetItem(100, null));
		}

		[Fact]
		public void OwnerIsPlayerOnSetItemIntoItemContainer() {
			var player = new Player();

			Container container = CreateContainer(10);
			player.Inventory[0] = container;
			Item item = CreateItem();
			container.Inventory[0] = item;

			Assert.Equal(2, player.Inventory.Count());
			Assert.Equal(player, container.Contained);
			Assert.Equal(player, container.Owner);
			Assert.Equal(container, item.Contained);
			Assert.Equal(player, item.Owner);
		}


		#region Nested type: TestInventory

		public class TestInventory : Inventory {
			public TestInventory(IContainer owner, uint slotsCount)
				: base(owner, slotsCount) {
			}

			public Item[] Items {
				get { return _items; }
			}
		}

		#endregion
	}
}