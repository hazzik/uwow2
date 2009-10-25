using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Hazzik {
	[XmlType("account")]
	public class Account {
		private static BigInteger bi_g = 7;
		private static BigInteger bi_N = new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);
		private readonly SHA1 sha1 = SHA1.Create();

		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int Expansion { get; set; }
		public virtual byte[] PasswordSalt { get; set; }
		public virtual byte[] PasswordVerifier { get; set; }
		public virtual byte[] SessionKey { get; set; }

		public void SetPassword(string password) {
			BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, new Random());
			PasswordSalt = bi_s.getBytes().Reverse();

			string p = (Name + ":" + password).ToUpper();
			byte[] pHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			byte[] x = H(bi_s.getBytes().Reverse().Concat(pHash));
			var bi_x = new BigInteger(x.Reverse());
			BigInteger bi_v = bi_g.modPow(bi_x, bi_N);
			PasswordVerifier = bi_v.getBytes().Reverse();
		}

		private byte[] H(IEnumerable<byte> bytes) {
			return sha1.ComputeHash(bytes.ToArray());
		}
	}
}