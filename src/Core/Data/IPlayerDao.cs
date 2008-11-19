using System;
using Hazzik.Objects;

namespace Hazzik.Data {
	public interface IPlayerDao : IDao<Player>, IGuided<Player>, INamed<Player> {
	}
}