using System;
using NHibernate;

namespace Hazzik.Data.NH {
	public abstract class NHDao<T> : IDao<T> {
		private static readonly ISessionFactory sessionFactory = IoC
			.Resolve<IConfigurationFactory>()
			.CreateConfiguration()
			.BuildSessionFactory();

		private readonly ISession session = sessionFactory.OpenSession();

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

		protected ICriteria CreateCriteria() {
			return session.CreateCriteria(typeof(T));
		}
	}
}