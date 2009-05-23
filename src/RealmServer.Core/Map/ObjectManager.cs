using System;
using System.Collections.Generic;
using Hazzik.Net;
using Hazzik.Objects;

namespace Hazzik.Map {
	public class ObjectManager {
		private static readonly IDictionary<ulong, Player> _allLoggedPlayers = new Dictionary<ulong, Player>();
		private static readonly IDictionary<ulong, Positioned> _allObjects = new Dictionary<ulong, Positioned>();

		public static void Add(Positioned player) {
			if(!_allObjects.ContainsKey(player.Guid)) {
				_allObjects.Add(player.Guid, player);
			}
			if(player is Player && !_allLoggedPlayers.ContainsKey(player.Guid)) {
				_allLoggedPlayers.Add(player.Guid, (Player)player);
			}
		}

		public static IEnumerable<Player> GetPlayersNear(Positioned me) {
			return _allLoggedPlayers.Values;
		}

		public static IEnumerable<Positioned> GetSeenObjectsNear(Player me) {
			return _allObjects.Values;
		}

		public static void SendNearExceptMe(Positioned me, IPacket responce) {
			foreach(var player in GetPlayersNear(me)) {
				if(player != me && player.Session!=null) {
					player.Session.Client.Send(responce);
				}
			}
		}

		public static void SendNear(Positioned me, IPacket responce) {
			foreach(var player in GetPlayersNear(me)) {
				if(player.Session != null) {
					player.Session.Client.Send(responce);
				}
			}
		}

		public static void Remove(WorldObject obj) {
			_allObjects.Remove(obj.Guid);
			_allLoggedPlayers.Remove(obj.Guid);
		}
	}
}