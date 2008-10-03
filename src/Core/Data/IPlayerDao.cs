using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Objects;

namespace Hazzik.Data {
	public interface IPlayerDao : IDao<Player>, IGuided<Player>, INamed<Player> {
	}
}
