﻿using System;
using NHibernate;

namespace Hazzik.Data.NH {
    public abstract class NHDao<T> : IDao<T> where T : class {
        private static readonly ISessionFactory sessionFactory = IoC
            .Resolve<IConfigurationFactory>()
            .CreateConfiguration()
            .BuildSessionFactory();

        protected readonly ISession session = sessionFactory.OpenSession();

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
    }
}