using System;
using System.Data.Common;

namespace Hazzik.Data.SQLite {
	public class AccountDaoSQLite : IAccountDao {
		private static readonly DbProviderFactory _factory = DbProviderFactories.GetFactory("System.Data.SQLite");
		private DbConnection _conection;

		internal AccountDaoSQLite() {
		}

		public Account FindByName(string name) {
			_conection = _factory.CreateConnection();
			_conection.ConnectionString = @"data source=E:\WowwoW\uwow2\uwow2.sqlite";
			_conection.Open();
			var cmd = _conection.CreateCommand();
			cmd.CommandText = string.Format(@"
SELECT * FROM ACCOUNTS WHERE AccountName = '{0}'
", name);
			using(var r = cmd.ExecuteReader()) {
				while(r.Read()) {
					var acc = new Account {
						//	 ID = 
					};
					return acc;
				}
				return null;
			}
		}

		public void Save(Account account) {
			var cmd = _conection.CreateCommand();

			throw new NotImplementedException();
		}

		public void Delete(Account account) {
			throw new NotImplementedException();
		}

		public void SubmitChanges() {
			throw new NotImplementedException();
		}
	}
}