using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Data;
using Hazzik.Data.Xml;
using Hazzik.Objects;

namespace Hazzik {
	[System.Xml.Serialization.XmlType("account")]
	public class RealmAccount : Account {
		protected static readonly IRealmAccountDao _dao = new XmlRealmAccountDao();
		protected readonly IList<Player> _players = new List<Player>();

		public Player[] Players {
			get { return _players.ToArray(); }
		}

		public static RealmAccount Create(string name) {
			return new RealmAccount { Name = name };
		}

		public static RealmAccount FindByName(string name) {
			return _dao.FindByName(name);
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
	}
}
