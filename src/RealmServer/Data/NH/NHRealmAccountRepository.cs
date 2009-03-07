using System;
using System.Linq;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	class NHRealmAccountRepository : NHDao<RealmAccount>, IRealmAccountDao {
		public RealmAccount FindByName(string name) {
			var criteria = Session.CreateCriteria(typeof(RealmAccount));
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<RealmAccount>().FirstOrDefault();
		}
	}
}
