using System;
using Hazzik.Annotations;
using Hazzik.Objects;

namespace Hazzik.Guilds
{
    public class GuildMember
    {
        private readonly Guild guild;
        private readonly Player player;

        [UsedImplicitly]
        protected GuildMember()
        {
        }

        public GuildMember([NotNull] Player player, [NotNull] Guild guild)
        {
            if (player == null)
                throw new ArgumentNullException("player");
            if (guild == null)
                throw new ArgumentNullException("guild");
            this.player = player;
            this.guild = guild;
        }

        [NotNull]
        public Guild Guild
        {
            get { return guild; }
        }

        public uint Rank { get; set; }

        public uint Timestamp { get; set; }
    }
}