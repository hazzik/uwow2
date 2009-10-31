using System;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_QUESTGIVER_QUERY_QUEST)]
	internal class QuestgiverQueryQuest : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			var reader = packet.CreateReader();
			var guid = reader.ReadUInt64();
			var questId = reader.ReadUInt32();
			var a = reader.ReadByte();
		}

		#endregion
	}
}