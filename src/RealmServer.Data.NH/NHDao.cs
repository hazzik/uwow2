﻿using System;
using NHibernate;

namespace Hazzik.Data.NH {
	public abstract class NHDao<T> : IDao<T> {
		private static readonly ISession session = ServiceLocator.Resolve<ISessionFactoryFactory>().GetFactory().OpenSession();

		#region IDao<T> Members

		public void Delete(T entity) {
			session.Delete(entity);
		}

		public void Save(T entity) {
			session.SaveOrUpdate(entity);
		}

		public void SubmitChanges() {
			session.Flush();
		}

		#endregion

		public ICriteria CreateCriteria() {
			return session.CreateCriteria(typeof(T));
		}
	}
}