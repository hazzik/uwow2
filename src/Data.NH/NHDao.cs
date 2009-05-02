using System;
using NHibernate;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public abstract class NHDao<T> : IDao<T> {
		private static readonly ISessionFactory _factory = GetFactory();
		private static readonly ISession _session = _factory.OpenSession();

		#region IDao<T> Members

		public void Delete(T entity) {
			_session.Delete(entity);
		}

		public void Save(T entity) {
			_session.SaveOrUpdate(entity);
		}

		public void SubmitChanges() {
			_session.Flush();
		}

		#endregion

		private static ISessionFactory GetFactory() {
			var cfg = new Configuration();
			return cfg.Configure().BuildSessionFactory();
		}

		public ICriteria CreateCriteria() {
			return _session.CreateCriteria(typeof(T));
		}
	}
}