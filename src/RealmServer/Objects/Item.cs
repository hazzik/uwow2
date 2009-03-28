using System;

namespace Hazzik.Objects {
	public partial class Item : WorldObject {
		private readonly ItemTemplate _template;

		public Item(ItemTemplate template)
			: this(template, (int)UpdateFields.ITEM_END) {
		}

		protected Item(ItemTemplate template, int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Item;
			_template = template;
			Entry = (uint)template.Id;
			Durability = (uint)template.MaxDurability;
			MaxDurability = (uint)template.MaxDurability;
			Flags = (uint)template.Flags;
			Duration = (uint)template.AttackTime;
			StackCount = (uint)template.MaxAmount;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Item; }
		}

		public WorldObject Owner { get { return Contained != null ? Contained.Owner : null; } }

		public IContainer Contained { get; set; }

		public ItemTemplate Template {
			get { return _template; }
		}

		public void Destroy() {
			if(Owner is Player) {
				((Player)Owner).Client.Send(GetDestroyObjectPkt());
			}
		}

		public bool CanStack(Item srcItem) {
			return Entry == srcItem.Entry && StackCount != Template.MaxAmount;
		}
	}
}