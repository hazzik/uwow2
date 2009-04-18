using System;
using Hazzik.Dbc;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class BankBagsInventory : InventoryWrapper, IBankBagsInventory {
		private const byte MaxBagSlots = 7;

		private readonly Player _player;

		public BankBagsInventory(Player player)
			: base(player.Inventory, 67, 0) {
			_player = player;
		}

		#region IBankBagsInventory Members

		public void BuySlot() {
			uint cost = BankSlotPricesRepository.GetCost(Slots + 1);
			if(Slots >= MaxBagSlots || _player.Coinage < cost) {
				return;
			}
			Slots++;
			_player.Coinage -= cost;
		}

		#endregion
	}
}