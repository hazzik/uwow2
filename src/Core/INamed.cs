using System;
namespace Hazzik {
	public interface INamed {
		object GetByName(string name);
	}
	public interface INamed<T> {
		T GetByName(string name);
	}
}
