using System;

namespace Hazzik {
	public class Account {
		public Guid ID { get; set; }
		public string Name { get; set; }
		public int Expansion { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordVerifier { get; set; }
		public byte[] SessionKey { get; set; }
	}
}