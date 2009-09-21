using System;
using System.Collections.Generic;
using Hazzik.Objects;

namespace Hazzik.Map {
	public class ObjectManager {
		private static readonly IDictionary<ulong, Player> AllLoggedPlayers = new Dictionary<ulong, Player>();
		private static readonly IDictionary<ulong, Positioned> AllObjects = new Dictionary<ulong, Positioned>();

		public static void Add(Positioned player) {
			if(!AllObjects.ContainsKey(player.Guid)) {
				AllObjects.Add(player.Guid, player);
			}
			if(player is Player && !AllLoggedPlayers.ContainsKey(player.Guid)) {
				AllLoggedPlayers.Add(player.Guid, (Player)player);
			}
		}

		public static IEnumerable<Player> GetPlayersNear(Positioned me) {
			return AllLoggedPlayers.Values;
		}

		public static IEnumerable<Positioned> GetSeenObjectsNear(Player me) {
			return AllObjects.Values;
		}

		public static void Remove(WorldObject obj) {
			AllObjects.Remove(obj.Guid);
			AllLoggedPlayers.Remove(obj.Guid);
		}
	}
}