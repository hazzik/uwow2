using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;

namespace Hazzik.Data.NH {
    public class FluentConfigurationFactory : IConfigurationFactory {
        #region IConfigurationFactory Members

        public Configuration CreateConfiguration() {
            return Fluently
                .Configure()
                .Database(SQLiteConfiguration
                              .Standard
                              .ConnectionString(@"Data Source=\uwow2\uwow2\src\uwow.s3db;Version=3")
                              .ProxyFactoryFactory<ProxyFactoryFactory>()
                              .ShowSql())
                .Mappings(m => m.FluentMappings
                                   .AddFromAssemblyOf<FluentConfigurationFactory>()
                                   .ExportTo(@"f:\uwow2\uwow2\src\"))
                .BuildConfiguration();
        }

        #endregion
    }
}