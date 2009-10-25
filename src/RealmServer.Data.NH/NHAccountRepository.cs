﻿using System;
using System.Linq;
using JetBrains.Annotations;
using NHibernate;
using NHibernate.Criterion;

namespace Hazzik.Data.NH {
	[UsedImplicitly]
	public class NHAccountRepository : NHDao<Account>, IAccountRepository {
		#region IAccountRepository Members

		public Account FindByName(string name) {
			ICriteria criteria = CreateCriteria();
			criteria.Add(Restrictions.Eq("Name", name.ToUpper()));
			return criteria.List<Account>().FirstOrDefault();
		}

		#endregion
	}
}