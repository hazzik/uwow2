using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			return new AccountDaoXml();
		}

		public IPlayerDao GetPlayerDao() {
			throw new NotImplementedException();
		}
	}
}
