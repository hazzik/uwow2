using System;
using Hazzik.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Guilds
{
    [WorldPacketHandler(WMSG.CMSG_GUILD_ADD_RANK)]
    public class GuildAddRank : IPacketDispatcher
    {
        #region IPacketDispatcher Members

        public void Dispatch(ISession session, IPacket packet)
        {
            var reader = packet.CreateReader();
            var rankName = reader.ReadCString();
        }

        #endregion
    }
}