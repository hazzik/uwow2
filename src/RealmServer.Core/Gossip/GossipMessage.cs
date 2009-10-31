using System;
using System.Collections.Generic;
using Hazzik.Annotations;

namespace Hazzik.Gossip {
	public class GossipMessage {
		private readonly IList<GossipMenuItem> gossipMenu;
		private readonly IList<QuestsMenuItem> questsMenu;
		private readonly uint textId;

		public GossipMessage(uint textId, IList<GossipMenuItem> gossipMenu, IList<QuestsMenuItem> questsMenu) {
			this.textId = textId;
			this.gossipMenu = gossipMenu ?? new GossipMenuItem[0];
			this.questsMenu = questsMenu ?? new QuestsMenuItem[0];
		}

		public uint TextId {
			get { return textId; }
		}

		[NotNull]
		public IList<GossipMenuItem> GossipMenu {
			get { return gossipMenu; }
		}

		[NotNull]
		public IList<QuestsMenuItem> QuestsMenu {
			get { return questsMenu; }
		}
	}
}