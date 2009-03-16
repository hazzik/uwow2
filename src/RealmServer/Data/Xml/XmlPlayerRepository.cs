using System;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Data.Xml {
	public class XmlPlayerRepository : XmlDao<Player>, IPlayerRepository {
		#region ctors

		internal XmlPlayerRepository()
			: base("player") {
		}

		#endregion

		#region IPlayerRepository Members

		public Player FindByGuid(ulong guid) {
			return (from player in _entities
			        where player.Guid == guid
			        select player).FirstOrDefault();
		}

		public Player FindByName(string name) {
			name = name.ToUpper();
			return (from player in _entities
			        where player.Name == name
			        select player).FirstOrDefault();
		}

		#endregion
	}
}