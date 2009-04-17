using System;

namespace Hazzik.Objects {
	public class ItemFactory {
		public static Item Create(ItemTemplate template) {
			return template.IsContainer ? new Container(template) : new Item(template);
		}
	}
}