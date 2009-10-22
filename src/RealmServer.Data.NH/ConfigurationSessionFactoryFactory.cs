using System;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	internal class ConfigurationSessionFactoryFactory : IConfigurationFactory {
		#region IConfigurationFactory Members

		public Configuration CreateConfiguration() {
			var cfg = new Configuration();
			return cfg.Configure();
		}

		#endregion
	}
}