using System;
using NHibernate;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public abstract class NHDao<T> : IDao<T> {
		private readonly ISession _session;

		protected NHDao() {
			var cfg = new Configuration();
			var factory = cfg.Configure().BuildSessionFactory();
			_session = factory.OpenSession();
		}

		public ISession Session {
			get { return _session; }
		}

		public void Delete(T entity) {
			Session.Delete(entity);
		}

		public void Save(T entity) {
			Session.Save(entity);
		}

		public void SubmitChanges() {
			Session.Flush();
		}
	}
}