using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlAccountDao : XmlDao<Account>, IAccountDao {
		public XmlAccountDao() : base("account") {
		}

		public Account FindByName(string name) {
			return _entities.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
		}
	}
}