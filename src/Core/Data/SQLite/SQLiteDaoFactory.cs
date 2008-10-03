using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik.Data.SQLite {
	public class SQLiteDaoFactory : IDaoFactory {
		private static SQLiteDaoFactory _instance;
		public static SQLiteDaoFactory Instance {
			get {
				if(_instance == null) {
					_instance = new SQLiteDaoFactory();
				}
				return _instance;
			}
		}

		public IAccountDao GetAccountDao() {
			return new AccountDaoSQLite();
		}

		public IPlayerDao GetPlayerDao() {
			throw new NotImplementedException();
		}
	}
}
