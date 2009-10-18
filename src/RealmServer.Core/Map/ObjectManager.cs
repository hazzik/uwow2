using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Objects;

namespace Hazzik.Map {
	public class ObjectManager {
		private static readonly IDictionary<ulong, Player> allLoggedPlayers = new Dictionary<ulong, Player>();
		private static readonly IDictionary<ulong, Positioned> allObjects = new Dictionary<ulong, Positioned>();

		public static void Add(Positioned positioned) {
			if(!allObjects.ContainsKey(positioned.Guid)) {
				allObjects.Add(positioned.Guid, positioned);
			}
			if(positioned is Player && !allLoggedPlayers.ContainsKey(positioned.Guid)) {
				allLoggedPlayers.Add(positioned.Guid, (Player)positioned);
			}
		}

		public static IEnumerable<Player> GetPlayersNear(Positioned me) {
			return allLoggedPlayers.Values;
		}

		public static IEnumerable<Positioned> GetSeenObjectsNear(Player me) {
			return allObjects.Values.Where(o => o.IsSeenBy(me));
		}

		public static void Remove(WorldObject obj) {
			allObjects.Remove(obj.Guid);
			allLoggedPlayers.Remove(obj.Guid);
		}
	}
}