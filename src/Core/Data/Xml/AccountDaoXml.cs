using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using Hazzik.Helper;
using Hazzik.Data;

namespace Hazzik.Data.Xml {
	public class AccountDaoXml : IAccountDao {
		private List<DbAccount> _accounts = new List<DbAccount>();
		private FileInfo _fi = new FileInfo(@"..\..\..\accounts.xml");
		private XmlSerializer _serializer = new XmlSerializer(typeof(List<DbAccount>));
		
		internal AccountDaoXml() {
			if(_fi.Exists) {
				using(var s = _fi.Open(FileMode.Open, FileAccess.Read)) {
					_accounts = (List<DbAccount>)_serializer.Deserialize(s);
				}
			}
		}

		public DbAccount Create(string name) {
			var account = new DbAccount() {
				ID = Guid.NewGuid(),
				Name = name,
			};
			_accounts.Add(account);
			return account;
		}

		public DbAccount GetByName(string name) {
			return (from account in _accounts
					  where account.Name.ToUpper() == name.ToUpper()
					  select account).FirstOrDefault();
		}

		public void Save(DbAccount account) {
			if(!_accounts.Contains(account)) {
				_accounts.Add(account);
			}
		}

		public void Delete(DbAccount account) {
			_accounts.Remove(account);
		}

		public void SubmitChanges() {
			using(var s = _fi.Open(FileMode.Create, FileAccess.Write)) {
				_serializer.Serialize(s, _accounts);
			}
		}
	}
}
