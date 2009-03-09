using System;
using System.Linq;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	public class NHAccountRepository : NHDao<Account>,IAccountDao {
		public Account FindByName(string name) {
			var criteria = Session.CreateCriteria(typeof(Account));
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Account>().FirstOrDefault();
		}
	}
}
