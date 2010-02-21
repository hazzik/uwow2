using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Internal {
	[WorldPacketHandler(WMSG.CMSG_PING)]
	internal class PingDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			session.Send(GetPongPkt(packet.CreateReader().ReadUInt32()));
		}

		#endregion

		public static IPacket GetPongPkt(uint ping) {
			IPacket result = WorldPacketFactory.Create(WMSG.SMSG_PONG);
			BinaryWriter w = result.CreateWriter();
			w.Write(ping);
			return result;
		}
	}
}