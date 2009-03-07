using System;
using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlRealmAccountDao : XmlDao<RealmAccount>, IRealmAccountDao {
		public XmlRealmAccountDao() : base("account") {
		}

		public RealmAccount FindByName(string name) {
			return _entities.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
		}
	}
}