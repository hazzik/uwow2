using System;
using Hazzik.Attributes;
using Hazzik.Creatures;
using Hazzik.Data;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class CreatureHandler {
		[WorldPacketHandler(WMSG.CMSG_CREATURE_QUERY)]
		public static void HandleCreatureQuery(ISession client, IPacket packet) {
			var r = packet.CreateReader();
			var creatureId = r.ReadUInt32();
			var creature = Repository.CreatureTemplate.FindById(creatureId);
			if(creature != null) {
				client.Send(GetResponce(creature));
			}
		}

		private static IPacket GetResponce(CreatureTemplate template) {
			var packet = WorldPacketFactory.Create(WMSG.SMSG_CREATURE_QUERY_RESPONSE);
			var writer = packet.CreateWriter();
			writer.Write(template.Id);
			writer.WriteCString(template.Name);
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString("");
			writer.WriteCString(template.GuildName);
			writer.Write((uint)template.Flags);
			writer.Write((uint)template.Type);
			writer.Write((uint)template.Family);
			writer.Write((uint)template.Rank);
			writer.Write(0);
			writer.Write(0); // SpellGroupId
			writer.Write(template.DisplayId);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(1f);
			writer.Write(1f);
			writer.Write((byte)0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0);
			writer.Write(0); // id from CreatureMovement.dbc
			return packet;
		}
	}
}