using System;
using NHibernate;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public abstract class NHDao<T> : IDao<T> {
		private static readonly ISessionFactory factory = GetFactory();
		private static readonly ISession session = factory.OpenSession();

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

		private static ISessionFactory GetFactory() {
			var cfg = new Configuration();
			return cfg.Configure().BuildSessionFactory();
		}

		public ICriteria CreateCriteria() {
			return session.CreateCriteria(typeof(T));
		}
	}
}