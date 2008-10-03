using System;
using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlAccountDao : XmlDao<DbAccount>, IAccountDao {
		#region ctors

		internal XmlAccountDao() {
		}

		#endregion

		#region INamed<DbAccount> Members

		public DbAccount Create(string name) {
			var account = new DbAccount() {
				ID = Guid.NewGuid(),
				Name = name,
			};
			_entities.Add(account);
			return account;
		}

		public DbAccount GetByName(string name) {
			return (from account in _entities
					  where account.Name.ToUpper() == name.ToUpper()
					  select account).FirstOrDefault();
		}

		#endregion
	}
}
