using System;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public class ConfigurationFactory : IConfigurationFactory {
		#region IConfigurationFactory Members

		public Configuration CreateConfiguration() {
			var cfg = new Configuration();
			return cfg.Configure();
		}

		#endregion
	}
}