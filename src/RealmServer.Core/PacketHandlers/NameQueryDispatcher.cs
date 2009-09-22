using System;
using System.IO;
using System.Linq;
using Hazzik.Attributes;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.PacketHandlers {
	[PacketHandlerClass(WMSG.CMSG_NAME_QUERY)]
	public class NameQueryDispatcher : IPacketDispatcher {
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet) {
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();

			Player player = ObjectManager.GetPlayersNear(session.Player).FirstOrDefault(x => x.Guid == guid);

			if(player != null) {
				session.SendNameQueryResponce(player);
			}
		}

		#endregion
	}
}