using System;
using System.Linq;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	public class NHAuthAccountRepository : NHDao<AuthAccount>,IAuthAccountDao {
		public AuthAccount FindByName(string name) {
			var criteria = Session.CreateCriteria(typeof(AuthAccount));
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<AuthAccount>().FirstOrDefault();
		}
	}
}
