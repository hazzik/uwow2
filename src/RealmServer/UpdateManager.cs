using System.Collections.Generic;
using System.Linq;
using Hazzik.Map;
using Hazzik.Objects;

namespace Hazzik {
	public class UpdateManager {
		private readonly Player _player;
		private IDictionary<ulong, IUpdateBlock> _updateBuilders = new Dictionary<ulong, IUpdateBlock>();
		protected Timer2 _updateTimer;

		public UpdateManager(Player player) {
			_player = player;
			_updateTimer = new UpdateTimer(this);
		}

		public void UpdateObjects() {
			var updateBuilders = GetUpdateBuilders();
			if(updateBuilders.Count != 0) {
				_player.Client.Send(new UpdatePacketBuilder(updateBuilders).Build());
			}
		}

		protected ICollection<IUpdateBlock> GetUpdateBuilders() {
			return new[] { GetOutOfRange() }.Concat(_updateBuilders.Values).Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			var updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, x => GetUpdater(x));
			var outOfRange = _updateBuilders.Keys.Except(updateBuilders.Keys).ToList();
			_updateBuilders = updateBuilders;
			return new OutOfRangeBlock(outOfRange);
		}

		private IEnumerable<WorldObject> GetObjectsForUpdate() {
			var items = _player.Items.Where(x => x != null).Cast<WorldObject>();
			var seenObjects = ObjectManager.GetSeenObjectsNear(_player).Cast<WorldObject>();
			return items.Concat(seenObjects);
		}

		private IUpdateBlock GetUpdater(WorldObject obj) {
			IUpdateBlock result;
			if(!_updateBuilders.TryGetValue(obj.Guid, out result)) {
				return _updateBuilders[obj.Guid] = new ObjectUpdater(_player, obj);
			}
			return result;
		}

		public void StartUpdateTimer() {
			_updateTimer.Start();
		}

		#region Nested type: UpdateTimer

		private class UpdateTimer : Timer2 {
			private readonly UpdateManager _manager;

			public UpdateTimer(UpdateManager manager)
				: base(3000) {
				_manager = manager;
			}

			public override void OnTick() {
				_manager.UpdateObjects();
			}
		}

		#endregion
	}
}