using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Creatures;
using Hazzik.Data;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class CreatureHandler {
		[WorldPacketHandler(WMSG.CMSG_CREATURE_QUERY)]
		public static void HandleCreatureQuery(ISession session, IPacket packet) {
			BinaryReader r = packet.CreateReader();
			uint creatureId = r.ReadUInt32();
			CreatureTemplate creature = Repository.CreatureTemplate.FindById(creatureId);
			if(creature != null) {
				session.SendCreatureQueryResponce(creature);
			}
		}
	}
}