using System;
using System.Linq;
using Hazzik.Objects;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	internal class NHPlayerRepository : NHDao<Player>, IPlayerRepository {
		#region IPlayerRepository Members

		public Player FindByGuid(ulong guid) {
			var criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Guid", guid));
			return criteria.List<Player>().FirstOrDefault();
		}

		public Player FindByName(string name) {
			var criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Player>().FirstOrDefault();
		}

		#endregion
	}
}