using System;
using NHibernate;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public class ConfigurationSessionFactoryFactory : IConfigurationFactory {
		#region IConfigurationFactory Members

		public Configuration CreateConfiguration() {
			var cfg = new Configuration();
			return cfg.Configure();
		}

		#endregion
	}
}