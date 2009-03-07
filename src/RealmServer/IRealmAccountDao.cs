using System;

namespace Hazzik.Data {
	public interface IRealmAccountDao : IDao<RealmAccount>, INamedSearch<RealmAccount> {
	}
}