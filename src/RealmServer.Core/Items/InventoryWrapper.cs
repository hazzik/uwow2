using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Items {
	public class InventoryWrapper : IInventory {
		private readonly IInventory _inventory;

		public InventoryWrapper(IInventory inventory, int offset, uint slots) {
			_inventory = inventory;
			Offset = offset;
			Slots = slots;
		}

		public int Offset { get; private set; }

		#region IInventory Members

		public IContainer Container {
			get { return _inventory.Container; }
		}

		public virtual uint Slots { get; protected set; }

		public virtual Item this[int slot] {
			get { return _inventory[slot]; }
			set { _inventory[slot] = value; }
		}

		public virtual int GetAmount(int id) {
			return this.Where(x => x.Entry == id).Count();
		}

		public virtual void DestroyItem(int slot) {
			if(slot >= Offset && slot < Slots + Offset) {
				_inventory.DestroyItem(slot);
			}
		}

		public virtual int FindFreeSlot() {
			for(var i = 0; i < Slots; i++) {
				if(this[i + Offset] == null) {
					return i + Offset;
				}
			}
			return -1;
		}

		public virtual int FindFreeSlot(IEnumerable<int> slots) {
			throw new NotImplementedException();
		}

		IEnumerator<Item> IEnumerable<Item>.GetEnumerator() {
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		private IEnumerator<Item> GetEnumerator() {
			for(var i = 0; i < Slots; i++) {
				var item = _inventory[i + Offset];
				if(item != null) {
					yield return item;
				}
			}
		}

		#endregion
	}
}