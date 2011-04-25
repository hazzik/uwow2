using System;

namespace Hazzik {
	public static class IoC {
		private static IDependencyResolver instance;

		public static void Initialize(IDependencyResolver resolver) {
			instance = resolver;
		}

		public static T Resolve<T>() {
			return instance.Resolve<T>();
		}
	}
}