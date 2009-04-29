using System;
using Hazzik.Attributes;
using Hazzik.Net;
using Hazzik.Repositories;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class GameObjectHandler {
		[WorldPacketHandler(WMSG.CMSG_GAMEOBJECT_QUERY)]
		public static void HandleGameobjectQuery(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var id = reader.ReadUInt32();
			var template = GameObjectTemplateRepository.FindById(id);
			if(template != null) {
				client.Send(template.GetResponce());
			}
		}
	}
}
