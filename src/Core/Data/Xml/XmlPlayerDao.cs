using System;
using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlPlayerDao : XmlDao<DbPlayer>, IPlayerDao {
		#region ctors

		internal XmlPlayerDao() {

		}

		#endregion

		#region IGuided<DbPlayer> Members

		public DbPlayer GetByGuid(ulong guid) {
			return (from player in _entities
					  where player.Guid == guid
					  select player).FirstOrDefault();
		}

		#endregion

		#region INamed<DbPlayer> Members

		public DbPlayer Create(string name) {
			return new DbPlayer() {
				Name = name,
			};
		}

		public DbPlayer GetByName(string name) {
			name = name.ToUpper();
			return (from player in _entities
					  where player.Name == name
					  select player).FirstOrDefault();
		}

		#endregion
	}
}
