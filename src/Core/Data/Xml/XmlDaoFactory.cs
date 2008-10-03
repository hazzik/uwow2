namespace Hazzik.Data.Xml {
	public class XmlDaoFactory : IDaoFactory {
		private static XmlDaoFactory _instance;
		public static XmlDaoFactory Instance {
			get {
				if(null == _instance) {
					_instance = new XmlDaoFactory();
				}
				return _instance;
			}
		}

		private XmlDaoFactory() {
		}

		public IAccountDao GetAccountDao() {
			return new XmlAccountDao();
		}

		public IPlayerDao GetPlayerDao() {
			return new XmlPlayerDao();
		}
	}
}
