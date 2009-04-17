using System;

namespace Hazzik.Gossip {
	public class GossipMenuItem {
		public GossipMenuItem(uint menuId, GossipMenuIcon icon, bool inputBox, string text)
			: this(menuId, icon, inputBox, 0, text, string.Empty) {
		}

		public GossipMenuItem(uint menuId, GossipMenuIcon icon, bool inputBox, uint cost, string text)
			: this(menuId, icon, inputBox, cost, text, string.Empty) {
		}

		public GossipMenuItem(uint menuId, GossipMenuIcon icon, bool inputBox, uint cost, string text, string acceptText) {
			MenuId = menuId;
			Icon = icon;
			InputBox = inputBox;
			Cost = cost;
			Text = text;
			AcceptText = acceptText;
		}

		public uint MenuId { get; private set; }

		public GossipMenuIcon Icon { get; private set; }

		public bool InputBox { get; set; }

		public uint Cost { get; private set; }

		public string Text { get; private set; }

		public string AcceptText { get; private set; }
	}
}