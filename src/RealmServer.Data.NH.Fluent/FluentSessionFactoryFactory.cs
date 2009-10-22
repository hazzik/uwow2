using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Hazzik.Data.NH;
using NHibernate.Cfg;

namespace Hazzik.RealmServer.Data.NH.Fluent {
	public class FluentSessionFactoryFactory : IConfigurationFactory {
		#region IConfigurationFactory Members

		public Configuration CreateConfiguration() {
			return Fluently
				.Configure()
				.Database(SQLiteConfiguration
				          	.Standard
				          	.Driver("NHibernate.Driver.SQLite20Driver")
				          	.Dialect("NHibernate.Dialect.SQLiteDialect")
				          	.Provider("NHibernate.Connection.DriverConnectionProvider")
				          	.ConnectionString(@"Data Source=\uwow2\uwow2\src\uwow.s3db;Version=3")
				          	.QuerySubstitutions("true=1;false=0")
				          	.ShowSql())
				.Mappings(m => m.FluentMappings
				               	.AddFromAssemblyOf<FluentSessionFactoryFactory>()
				               	.ExportTo(@"g:\uwow2\uwow2\src\"))
				.BuildConfiguration();
		}

		#endregion
	}
}