using System;
using System.IO;
using System.Linq;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_GAMEOBJ_USE)]
	internal class GameObjectUseDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession client, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();
			var gameObject = ObjectManager
			                 	.GetSeenObjectsNear(client.Player)
			                 	.Where(x => x.Guid == guid)
			                 	.FirstOrDefault() as GameObject;
			if(gameObject != null) {
				gameObject.Use(client);
			}
		}

		#endregion
	}
}