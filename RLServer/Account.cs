using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Helper;
using Hazzik.Data;

namespace Hazzik {
	public class Account {
		private DBAccount _account;
		public byte[] SS;

		public SRP6 SRP6 { get; set; }

		public Account(DBAccount account) {
			_account = account;
			this.SRP6 = new SRP6(true) {
				I = _account.Name,
				s = new BigInteger(_account.PasswordSalt.Reverse()),
				v = new BigInteger(_account.PasswordVerifier.Reverse()),
			};
		}
	}
}
