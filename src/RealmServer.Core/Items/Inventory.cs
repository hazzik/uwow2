using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Items {
	public abstract class Inventory : IInventory {
		protected Item[] _items;

		protected Inventory(IContainer container, uint slotsCount) {
			Container = container;
			Slots = slotsCount;
			_items = new Item[slotsCount];
		}

		#region IInventory Members

		public IContainer Container { get; private set; }

		public uint Slots { get; private set; }

		public Item this[int slot] {
			get { return GetItem(slot); }
			set { SetItem(slot, value); }
		}

		public int GetAmount(int id) {
			return (int)this.Where(x => x.Entry == id).Sum(x => x.StackCount);
		}

		IEnumerator<Item> IEnumerable<Item>.GetEnumerator() {
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void DestroyItem(int slot) {
			Item item = GetItem(slot);
			if(item != null) {
				item.Destroy();
			}
			SetItem(slot, null);
		}

		public abstract int FindFreeSlot();

		public int FindFreeSlot(IEnumerable<int> slots) {
			foreach(int slot in slots) {
				if(_items[slot] == null) {
					return slot;
				}
			}
			return -1;
		}

		#endregion

		public IEnumerator<Item> GetEnumerator() {
			foreach(Item item in _items) {
				if(null != item) {
					if(item is IContainer) {
						foreach(Item item2 in ((IContainer)item).Inventory) {
							yield return item2;
						}
					}
					yield return item;
				}
			}
		}

		public virtual void SetItem(int slot, Item item) {
			if(slot < 0 || slot >= Slots) {
				throw new ArgumentOutOfRangeException("slot");
			}
			if(null != item) {
				item.Contained = Container;
			}
			_items[slot] = item;
		}

		public virtual Item GetItem(int slot) {
			if(slot < 0 || slot >= Slots) {
				throw new ArgumentOutOfRangeException("slot");
			}
			return _items[slot];
		}
	}
}