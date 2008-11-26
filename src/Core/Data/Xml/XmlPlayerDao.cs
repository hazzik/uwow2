using System;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Data.Xml {
	public class XmlPlayerDao : XmlDao<Player>, IPlayerDao {
		#region ctors

		internal XmlPlayerDao() {
		}

		#endregion

		#region IGuided<DbPlayer> Members

		public Player FindByGuid(ulong guid) {
			return (from player in _entities
			        where player.Guid == guid
			        select player).FirstOrDefault();
		}

		#endregion

		#region INamed<DbPlayer> Members

		public Player Create(string name) {
			return new Player() {
				Name = name,
			};
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