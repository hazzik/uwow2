using System;

namespace Hazzik.Data {
	public interface IFindById<T> {
		T FindById(uint id);
	}
}