using System;
using System.Linq;
using Hazzik.Objects;
using JetBrains.Annotations;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	[UsedImplicitly]
	public class NHPlayerRepository : NHDao<Player>, IPlayerRepository {
		#region IPlayerRepository Members

		public Player FindByGuid(ulong guid) {
			ICriteria criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Guid", guid));
			return criteria.List<Player>().FirstOrDefault();
		}

		public Player FindByName(string name) {
			ICriteria criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Player>().FirstOrDefault();
		}

		#endregion
	}
}