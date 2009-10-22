using System;
using System.IO;
using Hazzik.Data;
using Hazzik.GameObjects;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_GAMEOBJECT_QUERY)]
	internal class GameObjectQueryDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			uint id = reader.ReadUInt32();
			GameObjectTemplate template = IoC.Resolve<IGameObjectTemplateRepository>().FindById(id);
			if(template != null) {
				session.SendGameObjectQueryResponce(template);
			}
		}

		#endregion
	}
}