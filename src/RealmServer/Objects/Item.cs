using System;

namespace Hazzik.Objects {
	public partial class Item : WorldObject {
		private readonly ItemTemplate _template;
		private IContainer _contained;
		private WorldObject _owner;

		public Item(ItemTemplate template)
			: this(template, (int)UpdateFields.ITEM_END) {
		}

		protected Item(ItemTemplate template, int updateMaskLength)
			: base(updateMaskLength) {
			Type |= ObjectTypes.Item;
			_template = template;
			Entry = (uint)template.Id;
			Durability = (uint)template.MaxDurability;
			Maxdurability = (uint)template.MaxDurability;
			Flags = (uint)template.Flags;
			Duration = (uint)template.AttackTime;
			StackCount = (uint)template.MaxAmount;
		}

		public override ObjectTypeId TypeId {
			get { return ObjectTypeId.Item; }
		}

		public WorldObject Owner {
			get { return _owner; }
			private set {
				_owner = value;
				SetUInt64(UpdateFields.ITEM_FIELD_OWNER, value != null ? value.Guid : 0);
			}
		}

		public IContainer Contained {
			get { return _contained; }
			set {
				if(value != null) {
					Owner = value.Owner;
					SetUInt64(UpdateFields.ITEM_FIELD_CONTAINED, value.Guid);
				}
				else {
					Owner = null;
					SetUInt64(UpdateFields.ITEM_FIELD_CONTAINED, 0);
				}
				_contained = value;
			}
		}

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