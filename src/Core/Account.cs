using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hazzik.Data;
using Hazzik.Data.SQLite;
using Hazzik.Helper;
using Hazzik.Objects;

namespace Hazzik {
	public partial class Account {
		private static readonly IAccountDao _dao = SQLiteDaoFactory.Instance.GetAccountDao();

		private SHA1 _sha1 = SHA1.Create();
		private static BigInteger bi_N = new BigInteger("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", 16);
		private static BigInteger bi_g = 7;

		public Account() {
		}

		public static Account Create(string name) {
			return new Account() { Name = name };
		}

		public static Account GetByName(string name) {
			return _dao.GetByName(name);
		}

		public void SetPassword(string password) {
			BigInteger bi_s = BigInteger.genPseudoPrime(256, 5, new Random());
			PasswordSalt = bi_s.getBytes().Reverse();

			var p = (Name + ":" + password).ToUpper();
			var pHash = _sha1.ComputeHash(Encoding.UTF8.GetBytes(p));
			var x = _sha1.ComputeHash(Utility.Concat(bi_s.getBytes().Reverse(), pHash));
			BigInteger bi_x = new BigInteger(x.Reverse());
			BigInteger bi_v = bi_g.modPow(bi_x, bi_N);
			PasswordVerifier = bi_v.getBytes().Reverse();
		}

		public void Save() {
			_dao.Save(this);
			_dao.SubmitChanges();
		}

		public void Delete() {
			_dao.Delete(this);
		}

		public Player GetPlayer(ulong guid) {
			return (from player in _players
			        where player.Guid == guid
			        select player).FirstOrDefault();
		}

		public void AddPlayer(Player player) {
			_players.Add(player);
		}

		public void DelPlayer(Player player) {
			_players.Remove(player);
		}

		public int PlayersCount {
			get { return _players.Count; }
		}
	}
}