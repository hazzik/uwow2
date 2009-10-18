using System;
using System.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.MSG_CORPSE_QUERY)]
	internal class CorpseQuery : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			IPacket pkt = GetCorpseQuery(session.Player.Corpse);
			session.Client.Send(pkt);
		}

		#endregion

		private static IPacket GetCorpseQuery(Corpse corpse) {
			IPacket pkt = WorldPacketFactory.Create(WMSG.MSG_CORPSE_QUERY);
			BinaryWriter writer = pkt.CreateWriter();
			writer.Write((byte)1);
			writer.Write(corpse.MapId);
			writer.Write(corpse.PosX);
			writer.Write(corpse.PosY);
			writer.Write(corpse.PosZ);
			writer.Write(corpse.MapId);
			return pkt;
		}
	}
}