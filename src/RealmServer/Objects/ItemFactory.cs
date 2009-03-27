using System;

namespace Hazzik.Objects {
	public class ItemFactory {
		public Item Create(ItemTemplate template,uint amount) {
			var item = template.IsContainer ? new Container(template) : new Item(template);
			item.StackCount = amount;

			return item;
		}
	}
}