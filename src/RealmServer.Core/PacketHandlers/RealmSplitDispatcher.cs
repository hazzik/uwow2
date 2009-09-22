using System;
using System.IO;
using Hazzik.Attributes;
using Hazzik.Net;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_REALM_SPLIT)]
	public class RealmSplitDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader r = packet.CreateReader();
			uint unk1 = r.ReadUInt32();

			session.SendRealmSplitPkt(unk1);
		}

		#endregion
	}
}