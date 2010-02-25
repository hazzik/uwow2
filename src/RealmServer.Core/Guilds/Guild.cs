using System.Collections.Generic;
using Hazzik.Annotations;

namespace Hazzik.Guilds
{
    public class Guild
    {
        public Guild()
        {
            Tabard = new GuildTabard();
            Ranks = new List<GuildRank>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [NotNull]
        public IList<GuildRank> Ranks { get; set; }

        [NotNull]
        public GuildTabard Tabard { get; set; }
    }
}