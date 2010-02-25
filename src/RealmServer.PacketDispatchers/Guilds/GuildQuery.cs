using System.IO;
using Hazzik.Guilds;
using Hazzik.IO;
using Hazzik.Net;

namespace Hazzik.RealmServer.PacketDispatchers.Guilds
{
    [WorldPacketHandler(WMSG.CMSG_GUILD_QUERY)]
    public class GuildQuery : IPacketDispatcher
    {
        #region IPacketDispatcher Members

        public void Dispatch(ISession session, IPacket packet)
        {
            BinaryReader reader = packet.CreateReader();
            int guildId = reader.ReadInt32();
            var guild = new Guild
                            {
                                Id = guildId,
                                Name = "guild"
                            };

            IPacketBuilder responce = WorldPacketFactory.Build(
                WMSG.SMSG_GUILD_QUERY_RESPONSE,
                writer =>
                    {
                        writer.Write(guild.Id);
                        writer.WriteCString(guild.Name);
                        foreach (GuildRank rank in guild.Ranks)
                            writer.WriteCString(rank.Name);
                        for (int i = 0; i < 10 - guild.Ranks.Count; i++)
                            writer.WriteCString(string.Empty);
                        writer.Write(guild.Tabard.EmblemStyle);
                        writer.Write(guild.Tabard.EmblemColor);
                        writer.Write(guild.Tabard.BorderStyle);
                        writer.Write(guild.Tabard.BorderColor);
                        writer.Write(guild.Tabard.BackgroundColor);
                        writer.Write(0); // NEW 3.0.2
                    });
            session.Send(responce);
        }

        #endregion
    }
}