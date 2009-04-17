using System;
using System.Linq;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	internal class NHAccountRepository : NHDao<Account>, IAccountRepository {
		#region IAccountRepository Members

		public Account FindByName(string name) {
			var criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Account>().FirstOrDefault();
		}

		#endregion
	}
}