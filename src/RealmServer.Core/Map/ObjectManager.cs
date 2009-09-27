using System;
using System.Collections.Generic;
using Hazzik.Objects;

namespace Hazzik.Map {
	public class ObjectManager {
		private static readonly IDictionary<ulong, Player> allLoggedPlayers = new Dictionary<ulong, Player>();
		private static readonly IDictionary<ulong, Positioned> allObjects = new Dictionary<ulong, Positioned>();

		public static void Add(Positioned player) {
			if(!allObjects.ContainsKey(player.Guid)) {
				allObjects.Add(player.Guid, player);
			}
			if(player is Player && !allLoggedPlayers.ContainsKey(player.Guid)) {
				allLoggedPlayers.Add(player.Guid, (Player)player);
			}
		}

		public static IEnumerable<Player> GetPlayersNear(Positioned me) {
			return allLoggedPlayers.Values;
		}

		public static IEnumerable<Positioned> GetSeenObjectsNear(Player me) {
			return allObjects.Values;
		}

		public static void Remove(WorldObject obj) {
			allObjects.Remove(obj.Guid);
			allLoggedPlayers.Remove(obj.Guid);
		}
	}
}