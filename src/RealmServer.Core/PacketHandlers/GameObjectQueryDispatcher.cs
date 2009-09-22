using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Data;
using Hazzik.GameObjects;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_GAMEOBJECT_QUERY)]
	public class GameObjectQueryDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			uint id = reader.ReadUInt32();
			GameObjectTemplate template = Repository.GameObjectTemplate.FindById(id);
			if(template != null) {
				session.SendGameObjectQueryResponce(template);
			}
		}

		#endregion
	}
}