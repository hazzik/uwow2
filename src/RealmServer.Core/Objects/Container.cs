using System;
using Hazzik.Items;
using Hazzik.Items.Inventories;

namespace Hazzik.Objects {
	public partial class Container : Item, IContainer {
		private readonly IInventory _inventory;

		public Container(ItemTemplate template)
			: base(template) {
			Type |= ObjectTypes.Container;
			_inventory = new ContainerInventory(this, (uint)template.ContainerSlots);
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Container; }
		}

		#region IContainer Members

		public IInventory Inventory {
			get { return _inventory; }
		}

		#endregion
	}
}