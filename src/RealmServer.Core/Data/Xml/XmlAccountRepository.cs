using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlAccountRepository : XmlDao<Account>, IAccountRepository {
		public XmlAccountRepository() : base("account") {
		}

		public Account FindByName(string name) {
			return _entities.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
		}
	}
}