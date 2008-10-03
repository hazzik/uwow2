using System;
namespace Hazzik.Data {
	public interface INamed {
		object GetByName(string name);
		object Create(string name);
	}
	public interface INamed<T> {
		T Create(string name);
		T GetByName(string name);
	}
}
