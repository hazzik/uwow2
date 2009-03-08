using System;
using Hazzik.Objects;

namespace Hazzik.Data.NH {
	public class NHPlayerRepository:NHDao<Player>, IPlayerDao {
		public Player FindByGuid(ulong guid) {
			throw new System.NotImplementedException();
		}

		public Player FindByName(string name) {
			throw new System.NotImplementedException();
		}
	}
}
