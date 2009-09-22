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
			Item item = !template.IsContainer
			            	? new Item(template)
			            	: new Container(template) { NumSlots = (uint)template.ContainerSlots };
			item.Entry = (uint)template.Id;
			item.Durability = (uint)template.MaxDurability;
			item.MaxDurability = (uint)template.MaxDurability;
			item.Flags = (uint)template.Flags;
			item.Duration = (uint)template.AttackTime;
			item.StackCount = (uint)template.MaxAmount;

			return item;
		}

		public void Destroy() {
			if(Owner is Player) {
				((Player)Owner).Session.SendDestroy(this);
			}
		}

		public bool CanStack(Item srcItem) {
			return srcItem != null && Entry == srcItem.Entry && StackCount != Template.MaxAmount;
		}
	}
}