using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Helper;

namespace UWoW {
	public class Account {
		static BigInteger bi_N = new BigInteger("B79B3E2A87823CAB8F5EBFBF8EB10108535006298B5BADBD5B53E1895E644B89", 16);
		static BigInteger bi_g = 7;
		static BigInteger bi_k = 3;
		static SHA1 sha1 = new SHA1Managed();

		private string _name;

		private byte[] _s;
		private byte[] _v;
		public void SetPassword(string password) {
			var p = (_name + ":" + password).ToUpper();
			var pHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			var x = sha1.ComputeHash(Utility.Concat(_s, pHash));
			var bi_x = new BigInteger(x.Reverse());
			var bi_v = bi_g.modPow(bi_x, bi_N);
		}
	}
}
