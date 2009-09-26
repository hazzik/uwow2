using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Map;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	public class UpdateManager {
		private readonly Player _player;
		private readonly Timer2 _updateTimer;
		private IDictionary<ulong, UpdateBlockBuilder> _updateBlockBuilders = new Dictionary<ulong, UpdateBlockBuilder>();

		public UpdateManager(Player player) {
			_player = player;
			_player.SetUpdateManager(this);
			_updateTimer = new UpdateTimer(this);
		}

		public void UpdateObjects() {
			ICollection<IUpdateBlock> updateBlocks = GetUpdateBlocks();
			_player.Session.SendUpdateObjects(new UpdatePacketBuilder(updateBlocks));
		}

		private ICollection<IUpdateBlock> GetUpdateBlocks() {
			IEnumerable<IUpdateBlock> allBlocks = new[] { GetOutOfRange() }.Concat(_updateBlockBuilders.Values.Select(x => x.CreateUpdateBlock()));
			return allBlocks.Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			Dictionary<ulong, UpdateBlockBuilder> updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, x => GetBuilder(x));
			List<ulong> outOfRange = _updateBlockBuilders.Keys.Except(updateBuilders.Keys).ToList();
			_updateBlockBuilders = updateBuilders;
			return new OutOfRangeBlockWriter(outOfRange);
		}

		private IEnumerable<WorldObject> GetObjectsForUpdate() {
			IEnumerable<WorldObject> items = _player.Inventory.Cast<WorldObject>();
			IEnumerable<WorldObject> seenObjects = ObjectManager.GetSeenObjectsNear(_player).Cast<WorldObject>();
			return items.Concat(seenObjects);
		}

		private UpdateBlockBuilder GetBuilder(WorldObject obj) {
			UpdateBlockBuilder result;
			if(!_updateBlockBuilders.TryGetValue(obj.Guid, out result)) {
				var updater = new UpdateBlockBuilder(_player, obj);
				_updateBlockBuilders[obj.Guid] = updater;
				return updater;
			}
			return result;
		}

		public void StartUpdateTimer() {
			_updateTimer.Start();
		}

		public void StopUpdateTimer() {
			_updateTimer.Stop();
		}
	}
}