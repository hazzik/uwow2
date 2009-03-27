using System;

namespace Hazzik.Objects {
	public partial class Container : Item, IContainer {
		private readonly IInventory _inventory;

		public Container(ItemTemplate template)
			: this(template, (int)UpdateFields.CONTAINER_END) {
		}

		protected Container(ItemTemplate template, int updateMaskLength)
			: base(template, updateMaskLength) {
			Type |= ObjectTypes.Container;
			NumSlots = (uint)template.ContainerSlots;
			_inventory = new Inventory(this, UpdateFields.CONTAINER_FIELD_SLOT_1, (uint)template.ContainerSlots);
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