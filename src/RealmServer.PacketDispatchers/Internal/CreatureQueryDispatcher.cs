using System;
using System.IO;
using Hazzik.Creatures;
using Hazzik.Data;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[PacketHandlerClass(WMSG.CMSG_CREATURE_QUERY)]
	internal class CreatureQueryDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader r = packet.CreateReader();
			uint creatureId = r.ReadUInt32();
			CreatureTemplate creature = Repository.CreatureTemplate.FindById(creatureId);
			if(creature != null) {
				session.SendCreatureQueryResponce(creature);
			}
		}

		#endregion
	}
}