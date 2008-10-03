using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Objects;

namespace Hazzik.Data {
	public class DbAccount {
		public Guid ID { get; set; }
		public string Name { get; set; }
		public int Expansion { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordVerifier { get; set; }
		public byte[] SessionKey { get; set; }

		private readonly List<Player> _players = new List<Player>();
		public Player[] Players { get { return _players.ToArray(); } }
		public void AddPlayer(Player player) {
			_players.Add(player);
		}
		public void DelPlayer(Player player) {
			_players.Remove(player);
		}
	}
}
