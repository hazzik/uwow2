using System;
using Hazzik.Objects;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
    public class NHPlayerRepository : NHDao<Player>, IPlayerRepository {
        #region IPlayerRepository Members

        public Player FindByGuid(ulong guid) {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("Guid", guid));
            return criteria.UniqueResult<Player>();
        }

        public Player FindByName(string name) {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
            return criteria.UniqueResult<Player>();
        }

        #endregion
    }
}