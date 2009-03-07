using System.Linq;

namespace Hazzik.Data.Xml {
	public class XmlAuthAccountDao : XmlDao<AuthAccount>, IAuthAccountDao {
		public XmlAuthAccountDao() : base("account") {
		}

		public AuthAccount FindByName(string name) {
			return _entities.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
		}
	}
}