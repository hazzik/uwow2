using System;

namespace Hazzik {
	public class Account {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int Expansion { get; set; }
		public virtual byte[] PasswordSalt { get; set; }
		public virtual byte[] PasswordVerifier { get; set; }
		public virtual byte[] SessionKey { get; set; }
	}
}