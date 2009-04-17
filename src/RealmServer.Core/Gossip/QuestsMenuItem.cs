using System;

namespace Hazzik.Gossip {
	public class QuestsMenuItem {
		public QuestsMenuItem(uint menuId, uint icon, string text) {
			Id = menuId;
			Icon = (uint)icon;
			Text = text;
		}

		public uint Id { get; private set; }

		public uint Icon { get; private set; }

		public string Text { get; private set; }
	}
}