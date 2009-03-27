using System;

namespace Hazzik.Objects {
	public class ItemFactory {
		public Item Create(ItemTemplate template) {
			var item = template.IsContainer ? new Container(template) : new Item(template);
			return item;
		}
	}
}