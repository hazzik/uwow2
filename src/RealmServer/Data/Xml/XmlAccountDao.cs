using System;
using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlAccountDao : XmlDao<Account>, IAccountDao {
		#region ctors

		internal XmlAccountDao() {
		}

		#endregion

		#region INamed<DbAccount> Members

		public Account FindByName(string name) {
			return (from account in _entities
			        where account.Name.ToUpper() == name.ToUpper()
			        select account).FirstOrDefault();
		}

		#endregion
	}
}