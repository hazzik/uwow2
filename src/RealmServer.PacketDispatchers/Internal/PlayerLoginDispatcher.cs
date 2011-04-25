using System.IO;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.RealmServer.PacketDispatchers.Internal
{
	[WorldPacketHandler(WMSG.CMSG_PLAYER_LOGIN)]
	internal class PlayerLoginDispatcher : IPacketDispatcher
	{
		#region IPacketDispatcher Members

		public void Dispatch(ISession session, IPacket packet)
		{
			BinaryReader reader = packet.CreateReader();
			ulong guid = reader.ReadUInt64();
			Player player = session.Account.GetPlayer(guid);
			
			session.Login(player);
		}

		#endregion
	}
}