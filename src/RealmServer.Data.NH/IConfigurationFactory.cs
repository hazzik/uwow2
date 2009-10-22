using System;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
	public interface IConfigurationFactory {
		Configuration CreateConfiguration();
	}
}