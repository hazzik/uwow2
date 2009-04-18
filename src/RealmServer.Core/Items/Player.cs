using System;
using Hazzik.Items;

namespace Hazzik.Objects {
	public partial class Player {
		private readonly BankInventory _bank;
		private readonly BankBagsInventory _bankBags;
		private readonly KeyRingInventory _keyRing;

		public IInventory Bank {
			get { return _bank; }
		}

		public IBankBagsInventory BankBags {
			get { return _bankBags; }
		}

		public IInventory KeyRing {
			get { return _keyRing; }
		}
	}
}