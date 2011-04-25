using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Hazzik.Data;
using Hazzik.Data.NH;

namespace Hazzik
{
    internal sealed class DependencyResolver : IDependencyResolver
    {
        private readonly WindsorContainer container = new WindsorContainer();

        public DependencyResolver()
        {
            container.Register(Component.For<IConfigurationFactory>().ImplementedBy<ConfigurationFactory>(),
                               Component.For<IAccountRepository>().ImplementedBy<NHAccountRepository>());
        }

        #region IDependencyResolver Members

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        #endregion
    }
}