using System;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
    public class NHAccountRepository : NHDao<Account>, IAccountRepository {
        #region IAccountRepository Members

        public Account FindByName(string name) {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
            return criteria.UniqueResult<Account>();
        }

        #endregion
    }
}