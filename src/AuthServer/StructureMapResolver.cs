using System;
using Hazzik.Data;
using Hazzik.Data.NH;
using StructureMap;

namespace Hazzik {
	internal sealed class StructureMapResolver : IDependencyResolver
	{
		static StructureMapResolver()
		{
			ObjectFactory.Configure(config =>
			                        {
			                        	config.ForRequestedType<IConfigurationFactory>().AddConcreteType<ConfigurationFactory>();
			                        	config.ForRequestedType<IAccountRepository>().AddConcreteType<NHAccountRepository>();
			                        });
		}

		public T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}
	}
}