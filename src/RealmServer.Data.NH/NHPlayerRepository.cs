using System;
using Hazzik.Objects;

namespace Hazzik.Data.NH {
    public class NHPlayerRepository : NHDao<Player>, IPlayerRepository {
        #region IPlayerRepository Members

        public Player FindByGuid(ulong guid) {
            return session.QueryOver<Player>()
                .Where(x => x.Guid == guid)
                .SingleOrDefault();
        }

        public Player FindByName(string name) {
            return session.QueryOver<Player>()
                .Where(x => x.Name == name.ToUpper())
                .SingleOrDefault();
        }

        #endregion
    }
}