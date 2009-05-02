using System;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.GameObjects;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass]
	public class GameObjectHandler {
		[WorldPacketHandler(WMSG.CMSG_GAMEOBJECT_QUERY)]
		public static void HandleGameObjectQuery(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var id = reader.ReadUInt32();
			var template = Data.Repository.GameObjectTemplate.FindById(id);
			if(template != null) {
				client.Send(template.GetResponce());
			}
		}

		[WorldPacketHandler(WMSG.CMSG_GAMEOBJ_USE)]
		public static void HandleGameObjectUse(ISession client, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var go = ObjectManager.GetSeenObjectsNear(client.Player).Where(x => x.Guid == guid).FirstOrDefault() as GameObject;
			new ChairHandler(go).Use(client.Player);
		}
	}
}
