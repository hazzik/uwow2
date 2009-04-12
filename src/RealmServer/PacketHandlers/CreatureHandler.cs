using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Repositories;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class CreatureHandler {
		[WorldPacketHandler(WMSG.CMSG_CREATURE_QUERY)]
		public static void HandleCreatureQuery(ISession client, IPacket packet) {
			var r = packet.CreateReader();
			var creatureId = r.ReadUInt32();
			var creature = CreatureTemplateRepository.FindById(creatureId);
			if(creature != null) {
				client.Send(creature.GetResponce());
			}
		}
	}
}