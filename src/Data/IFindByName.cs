using System;

namespace Hazzik.Data {
	public interface IFindByName<T> {
		T FindByName(string name);
	}
}