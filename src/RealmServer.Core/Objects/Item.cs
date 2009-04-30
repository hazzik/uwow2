using System;
using Hazzik.Items;

namespace Hazzik.Objects {
	public partial class Item : WorldObject {
		private readonly ItemTemplate _template;

		public Item(ItemTemplate template) {
			_template = template;
			Type |= ObjectTypes.Item;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Item; }
		}

		public WorldObject Owner {
			get { return Contained != null ? Contained.Owner : null; }
		}

		public IContainer Contained { get; set; }

		public ItemTemplate Template {
			get { return _template; }
		}

		public static Item Create(ItemTemplate template) {
			if(template == null) {
				return null;
			}
			return !template.IsContainer
			       	? new Item(template) {
			       		Entry = (uint)template.Id,
			       		Durability = (uint)template.MaxDurability,
			       		MaxDurability = (uint)template.MaxDurability,
			       		Flags = (uint)template.Flags,
			       		Duration = (uint)template.AttackTime,
			       		StackCount = (uint)template.MaxAmount,
			       	}
			       	: new Container(template) {
			       		Entry = (uint)template.Id,
			       		Durability = (uint)template.MaxDurability,
			       		MaxDurability = (uint)template.MaxDurability,
			       		Flags = (uint)template.Flags,
			       		Duration = (uint)template.AttackTime,
			       		StackCount = (uint)template.MaxAmount,
			       		NumSlots = (uint)template.ContainerSlots,
			       	};
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