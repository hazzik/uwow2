using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using Hazzik.Helper;

namespace Hazzik {
	public class AccountDao {
		private static BigInteger bi_N = new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);
		private static BigInteger bi_g = 7;

		private static AccountDao _instance;
		public static AccountDao Instance {
			get {
				if(null == _instance) {
					_instance = new AccountDao();
				}
				return _instance;
			}
		}

		private AccountDao() {
			if(_fi.Exists) {
				using(var s = _fi.Open(FileMode.Open, FileAccess.Read)) {
					_accounts = (List<DbAccount>)_serializer.Deserialize(s);
				}
			}
		}

		private List<DbAccount> _accounts = new List<DbAccount>();
		private FileInfo _fi = new FileInfo(@"..\..\..\accounts.xml");
		private XmlSerializer _serializer = new XmlSerializer(typeof(List<DbAccount>));
		private SHA1 sha1 = SHA1.Create();

		public DbAccount CreateAccount(string name) {
			var account = new DbAccount() {
				ID = Guid.NewGuid(),
				Name = name,
			};
			_accounts.Add(account);
			return account;
		}

		public DbAccount GetAccountByName(string name) {
			return (from account in _accounts
					  where account.Name.ToUpper() == name.ToUpper()
					  select account).FirstOrDefault();
		}

		public void SetPassword(DbAccount account, string password) {
			BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, new Random());
			account.PasswordSalt = bi_s.getBytes().Reverse();

			var p = (account.Name + ":" + password).ToUpper();
			var pHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			var x = sha1.ComputeHash(Utility.Concat(bi_s.getBytes().Reverse(), pHash));
			BigInteger bi_x = new BigInteger(x.Reverse());
			BigInteger bi_v = bi_g.modPow(bi_x, bi_N);
			account.PasswordVerifier = bi_v.getBytes().Reverse();
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
