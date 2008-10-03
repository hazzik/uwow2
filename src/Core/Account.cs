using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Helper;
using System.Security.Cryptography;
using Hazzik.Data;

namespace Hazzik {
	public class Account {
		private static readonly IAccountDao _dao = Hazzik.Data.SQLite.SQLiteDaoFactory.Instance.GetAccountDao();

		private SHA1 _sha1 = SHA1.Create();
		private static BigInteger bi_N = new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);
		private static BigInteger bi_g = 7;
		
		private DbAccount _account;

		public DbAccount DbAccount {
			get { return _account; }
		}

		public Account(DbAccount account) {
			_account = account;
		}

		public Account()
			: this(new DbAccount()) {
		}

		public static Account Create(string name) {
			return new Account(_dao.Create(name));
		}

		public static Account GetByName(string name) {
			var acc = _dao.GetByName(name);
			return acc != null ? new Account(acc) : null;
		}

		public void SetPassword(string password) {
			BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, new Random());
			_account.PasswordSalt = bi_s.getBytes().Reverse();

			var p = (_account.Name + ":" + password).ToUpper();
			var pHash = _sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			var x = _sha1.ComputeHash(Utility.Concat(bi_s.getBytes().Reverse(), pHash));
			BigInteger bi_x = new BigInteger(x.Reverse());
			BigInteger bi_v = bi_g.modPow(bi_x, bi_N);
			_account.PasswordVerifier = bi_v.getBytes().Reverse();
		}

		public void Save() {
			_dao.Save(_account);
			_dao.SubmitChanges();
		}

		public void Delete() {
			_dao.Delete(_account);
			_account = null;
		}
	}
}
