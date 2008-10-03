using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Objects;

namespace Hazzik {
	public partial class Account {
		public Guid ID { get; set; }
		public string Name { get; private set; }
		public int Expansion { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordVerifier { get; set; }
		public byte[] SessionKey { get; set; }

		private readonly IList<Player> _players = new List<Player>();
		public Player[] Players {
			get { return _players.ToArray(); }
		}
	}
}
