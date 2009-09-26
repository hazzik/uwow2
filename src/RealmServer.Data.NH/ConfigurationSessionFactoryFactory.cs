using System;
using NHibernate;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public class ConfigurationSessionFactoryFactory : ISessionFactoryFactory {
		public ISessionFactory GetFactory() {
			var cfg = new Configuration();
			return cfg.Configure().BuildSessionFactory();
		}
	}
}