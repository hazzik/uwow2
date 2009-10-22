using System;
using System.IO;
using Hazzik.Data;
using Hazzik.Gossip;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_NPC_TEXT_QUERY)]
	internal class NpcTextQueryDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			uint textId = reader.ReadUInt32();
			ulong targetGuid = reader.ReadUInt64();

			NpcTexts text = IoC.Resolve<INpcTextRepository>().FindById(textId);
			if(text != null) {
				session.SendNpcTextUpdate(text);
			}
		}

		#endregion
	}
}