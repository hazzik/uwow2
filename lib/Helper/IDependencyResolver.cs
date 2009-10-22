using System;

namespace Hazzik {
	public interface IDependencyResolver {
		T Resolve<T>();
	}
}