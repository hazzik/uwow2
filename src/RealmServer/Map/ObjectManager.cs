using System;
using System.Collections.Generic;
using Hazzik.Objects;

namespace Hazzik.Map {
	public class ObjectManager {
		private static readonly IDictionary<ulong, Player> _allLoggedPlayers = new Dictionary<ulong, Player>();
		private static readonly IDictionary<ulong, Positioned> _allObjects = new Dictionary<ulong, Positioned>();

		public static void Add(Player player) {
			if(!_allObjects.ContainsKey(player.Guid)) {
				_allObjects.Add(player.Guid, player);
			}
			if(!_allLoggedPlayers.ContainsKey(player.Guid)) {
				_allLoggedPlayers.Add(player.Guid, player);
			}
		}

		public static IEnumerable<Player> GetPlayersNear(Player me) {
			return _allLoggedPlayers.Values;
		}

		public static IEnumerable<Positioned> GetObjectsNear(Player me) {
			return _allObjects.Values;
		}
	}
}