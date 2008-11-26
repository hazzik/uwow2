using System;

namespace Hazzik.Data {
	public interface INamedSearch<T> {
		T FindByName(string name);
	}
}