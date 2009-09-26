using System;
using Hazzik.Objects;

namespace Hazzik.Items.Inventories {
	public class KeyRingInventory : InventoryWrapper {
		private readonly Player _player;

		public KeyRingInventory(Player player)
			: base(player.Inventory, 91, 0) {
			_player = player;
		}

		public override uint Slots {
			get {
				uint level = _player.Level;
				return (uint)(level >= 60 ? 16 : level >= 50 ? 12 : level >= 40 ? 8 : 4);
			}
		}
	}
}