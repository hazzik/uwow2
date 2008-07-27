using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Helper;
using UWoW.Data;

namespace UWoW {
	public class Account {
		private DBAccount _account;
		private SRP6 _srp6 = new SRP6(true);
		
		public SRP6 SRP6 {
			get { return _srp6; }
			set { _srp6 = value; }
		}

		public Account(DBAccount account) {
			_account = account;
			_srp6.I = _account.Name;
			_srp6.s = new BigInteger(_account.PasswordSalt.Reverse());
			_srp6.v = new BigInteger(_account.PasswordVerifier.Reverse());
		}
	}
}
