using System;
using Hazzik.Objects;

namespace Hazzik.Data {
	public interface IPlayerRepository : IDao<Player>, IFindByGuid<Player>, IFindByName<Player> {
	}
}