using System;
using System.Collections.Generic;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Net;

namespace Hazzik.Gossip {
	public class GossipMessage {
		private uint _textId;
		private IList<GossipMenuItem> _gossipMenu;
		private IList<QuestsMenuItem> _questsMenu;

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

	[PacketHandlerClass]
	public class GossipHandler {
		[WorldPacketHandler(WMSG.CMSG_GOSSIP_HELLO)]
		public static void HandleGossipHello(ISession session, IPacket packet) {
			ulong targetGuid = packet.CreateReader().ReadUInt64();
			var message = new GossipMessage(2, new List<GossipMenuItem> {
				new GossipMenuItem(2, GossipMenuIcon.Banker, false, "Hello?"),
			}, null);
			session.SendGossipMessage(targetGuid, message);
		}

		[WorldPacketHandler(WMSG.CMSG_GOSSIP_SELECT_OPTION)]
		public static void HandleGossipSelectOption(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong targetGuid = reader.ReadUInt64();
			uint unk1 = reader.ReadUInt32();
			uint option = reader.ReadUInt32();
		}

		[WorldPacketHandler(WMSG.CMSG_NPC_TEXT_QUERY)]
		public static void HandleNpcTextQuery(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var textId = reader.ReadUInt32();
			var targetGuid = reader.ReadUInt64();
			
			var text = Repository.NpcText.FindById(textId);
			if(text != null) {
				session.SendNpcTextUpdate(text);
			}
		}
	}
}