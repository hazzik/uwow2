using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Hazzik.Data;
using Hazzik.Data.Fake;
using Hazzik.Data.NH;

namespace Hazzik {
	internal sealed class DependencyResolver : IDependencyResolver {
	    private static readonly WindsorContainer container = new WindsorContainer();

	    static DependencyResolver()
	    {
	        container.Register(
	            Component.For<IConfigurationFactory>().ImplementedBy<FluentConfigurationFactory>(),
	            Component.For<IAccountRepository>().ImplementedBy<NHAccountRepository>(),
	            Component.For<IPlayerRepository>().ImplementedBy<NHPlayerRepository>(),
	            Component.For<IGameObjectTemplateRepository>().ImplementedBy<NHGameObjectTemplateRepository>(),
	            Component.For<ICreatureTemplateRepository>().ImplementedBy<FakeCreatureTemplateRepository>(),
	            Component.For<IItemTemplateRepository>().ImplementedBy<FakeItemTemplateRepository>(),
	            Component.For<INpcTextRepository>().ImplementedBy<FakeNpcTextRepository>());
	    }

		public T Resolve<T>() {
			return container.Resolve<T>();
		}
	}
}