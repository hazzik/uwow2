using System;

namespace Hazzik {
	public abstract class ServiceLocator {
		private static ServiceLocator _instance;

		public static void Initialize(ServiceLocator locator) {
			_instance = locator;
		}

		public static T Resolve<T>() {
			return _instance.ResolveImpl<T>();
		}

		protected abstract T ResolveImpl<T>();
	}
}