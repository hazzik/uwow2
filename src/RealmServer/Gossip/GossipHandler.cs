using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Hazzik.Attributes;
using Hazzik.Creatures;
using Hazzik.Net;

namespace Hazzik.Gossip {
	[PacketHandlerClass]
	public class GossipHandler {
		[WorldPacketHandler(WMSG.CMSG_GOSSIP_HELLO)]
		public static void HandleGossipHello(ISession client, IPacket packet) {
			var targetGuid = packet.CreateReader().ReadUInt64();
			client.Send(GetGossipMessagePkt(targetGuid, 2, new List<GossipMenuItem> {
				new GossipMenuItem(1, GossipMenuIcon.Gossip, false, "Hello?"),
				new GossipMenuItem(2, GossipMenuIcon.Vendor, false, "Hello?"),
			}, null));
		}

		[WorldPacketHandler(WMSG.CMSG_GOSSIP_SELECT_OPTION)]
		public static void HandleGossipSelectOption(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var targetGuid = reader.ReadUInt64();
			var unk1 = reader.ReadUInt32();
			var option = reader.ReadUInt32();
		}

		[WorldPacketHandler(WMSG.CMSG_NPC_TEXT_QUERY)]
		public static void HandleNpcTextQuery(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var textId = reader.ReadUInt32();
			var targetGuid = reader.ReadUInt64();
			client.Send(GetNpcTextUpdatePkt(textId));
		}

		private static IPacket GetNpcTextUpdatePkt(uint textId) {
			var responce = new WorldPacket(WMSG.SMSG_NPC_TEXT_UPDATE);
			var writer = responce.CreateWriter();
			writer.Write(textId);
			var texts = new[] {
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
				new NpcText { Emote = new int[3,2], Text0 ="Hello sir?",Text1 ="Hello sir?" },
			};
			for(int i = 0; i < 8; i++) {
				var text = texts[i];
				writer.Write(text.Probability);
				writer.WriteCString(text.Text0);
				writer.WriteCString(text.Text1);
				writer.Write(text.Language);
				for(int j = 0; (j < 3); j++) {
					writer.Write(text.Emote[j, 0]);
					writer.Write(text.Emote[j, 1]);
				}
			}
			return responce;
		}


		public static IPacket GetGossipMessagePkt(ulong guid, uint textId, IList<GossipMenuItem> gossipMenu, IList<QuestsMenuItem> questsMenu) {
			var packet = new WorldPacket(WMSG.SMSG_GOSSIP_MESSAGE);
			var writer = packet.CreateWriter();
			writer.Write(guid);
			writer.Write(0);
			writer.Write(textId);
			if((gossipMenu != null) && (gossipMenu.Count > 0)) {
				writer.Write(gossipMenu.Count);
				foreach(var menuItem in gossipMenu) {
					writer.Write(menuItem.MenuId);
					writer.Write((byte)menuItem.Icon);
					writer.Write((byte)(menuItem.InputBox ? 1 : 0));
					writer.Write(menuItem.Cost);
					writer.WriteCString(menuItem.Text);
					writer.WriteCString(menuItem.AcceptText);
				}
			}
			else {
				writer.Write(0);
			}

			if((questsMenu != null) && (questsMenu.Count > 0)) {
				writer.Write(questsMenu.Count);
				foreach(var menuItem in questsMenu) {
					writer.Write(menuItem.Id);
					writer.Write(menuItem.Icon);
					writer.Write((uint)0);
					writer.Write(menuItem.Text);
				}
			}
			else {
				writer.Write(0);
			}
			return packet;
		}
	}
}
