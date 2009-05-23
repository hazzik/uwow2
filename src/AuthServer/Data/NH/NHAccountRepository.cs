using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	internal class NHAccountRepository : NHDao<Account>, IAccountDao {
		#region IAccountDao Members

		public Account FindByName(string name) {
			ICriteria criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Account>().FirstOrDefault();
		}

		#endregion
	}
}