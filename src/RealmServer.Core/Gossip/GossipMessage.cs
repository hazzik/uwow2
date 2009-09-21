using System;
using System.Collections.Generic;

namespace Hazzik.Gossip {
	public class GossipMessage {
		private readonly uint _textId;
		private readonly IList<GossipMenuItem> _gossipMenu;
		private readonly IList<QuestsMenuItem> _questsMenu;

		public GossipMessage(uint textId, IList<GossipMenuItem> gossipMenu, IList<QuestsMenuItem> questsMenu) {
			_textId = textId;
			_gossipMenu = gossipMenu;
			_questsMenu = questsMenu;
		}

		public uint TextId {
			get { return _textId; }
		}

		public IList<GossipMenuItem> GossipMenu {
			get { return _gossipMenu; }
		}

		public IList<QuestsMenuItem> QuestsMenu {
			get { return _questsMenu; }
		}
	}
}