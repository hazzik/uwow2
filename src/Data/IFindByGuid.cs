using System;

namespace Hazzik.Data {
	public interface IFindByGuid<T> {
		T FindByGuid(ulong guid);
	}
}