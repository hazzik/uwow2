using System;

namespace Hazzik.Data {
	public interface IGuidedSearch<T> {
		T FindByGuid(ulong guid);
	}
}