using System;
namespace Hazzik.Data {
	public interface INamed {
		object GetByName(string name);
	}
	public interface INamed<T> {
		T GetByName(string name);
	}
}
