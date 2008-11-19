using System;

namespace Hazzik.Data {
	public interface IGuided {
		object GetByGuid(ulong guid);
	}

	public interface IGuided<T> {
		T GetByGuid(ulong guid);
	}
}