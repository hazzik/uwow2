using System;
using System.Collections.Generic;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.Net;

namespace Hazzik.Gossip {
	[PacketHandlerClass]
	public class GossipHandler {
		[WorldPacketHandler(WMSG.CMSG_GOSSIP_HELLO)]
		public static void HandleGossipHello(ISession session, IPacket packet) {
			ulong targetGuid = packet.CreateReader().ReadUInt64();
			session.Client.Send(GetGossipMessagePkt(targetGuid, 2, new List<GossipMenuItem> {
				//new GossipMenuItem(1, GossipMenuIcon.Gossip, false, "Hello?"),
				new GossipMenuItem(2, GossipMenuIcon.Banker, false, "Hello?"),
			}, null));
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
				session.Client.Send(text.GetNpcTextUpdatePkt());
			}
		}

		public static IPacket GetGossipMessagePkt(ulong guid, uint textId, IList<GossipMenuItem> gossipMenu, IList<QuestsMenuItem> questsMenu) {
			var packet = WorldPacketFactory.Create(WMSG.SMSG_GOSSIP_MESSAGE);
			BinaryWriter writer = packet.CreateWriter();
			writer.Write(guid);
			writer.Write(0);
			writer.Write(textId);
			if((gossipMenu != null) && (gossipMenu.Count > 0)) {
				writer.Write(gossipMenu.Count);
				foreach(GossipMenuItem menuItem in gossipMenu) {
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
				foreach(QuestsMenuItem menuItem in questsMenu) {
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