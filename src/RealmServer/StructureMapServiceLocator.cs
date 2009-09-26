using System;
using Hazzik.Data;
using Hazzik.Data.Fake;
using Hazzik.Data.NH;
using Hazzik.RealmServer.Data.NH.Fluent;
using StructureMap;

namespace Hazzik {
	internal class StructureMapServiceLocator : ServiceLocator {
		static StructureMapServiceLocator() {
			ObjectFactory.Configure(config => {
			                        	config.ForRequestedType<ISessionFactoryFactory>().AddConcreteType<FluentSessionFactoryFactory>();
			                        	config.ForRequestedType<IAccountRepository>().AddConcreteType<NHAccountRepository>();
			                        	config.ForRequestedType<IPlayerRepository>().AddConcreteType<NHPlayerRepository>();
			                        	config.ForRequestedType<IGameObjectTemplateRepository>().AddConcreteType<NHGameObjectTemplateRepository>();
			                        	config.ForRequestedType<ICreatureTemplateRepository>().AddConcreteType<FakeCreatureTemplateRepository>();
			                        	config.ForRequestedType<IItemTemplateRepository>().AddConcreteType<FakeItemTemplateRepository>();
			                        	config.ForRequestedType<INpcTextRepository>().AddConcreteType<FakeNpcTextRepository>();
			                        });
		}

		protected override T ResolveImpl<T>() {
			return ObjectFactory.GetInstance<T>();
		}
	}
}