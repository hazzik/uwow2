using System;

namespace Hazzik.Data.NH {
    public class NHAccountRepository : NHDao<Account>, IAccountRepository {
        #region IAccountRepository Members

        public Account FindByName(string name) {
            return session.QueryOver<Account>()
                .Where(x => x.Name == name.ToUpper())
                .SingleOrDefault();
        }

        #endregion
    }
}