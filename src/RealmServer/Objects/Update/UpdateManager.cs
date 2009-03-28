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
			_updateTimer = new UpdateTimer(this);
		}

		public void UpdateObjects() {
			var updateBlocks = GetUpdateBlocks();
			if(updateBlocks.Count != 0) {
				_player.Client.Send(new UpdatePacketBuilder(updateBlocks).Build());
			}
		}

		private ICollection<IUpdateBlock> GetUpdateBlocks() {
			var allBlocks = new[] { GetOutOfRange() }.Concat(_updateBlockBuilders.Values.Select(x=>x.CreateUpdateBlock()));
			return allBlocks.Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			var updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, x => GetBuilder(x));
			var outOfRange = _updateBlockBuilders.Keys.Except(updateBuilders.Keys).ToList();
			_updateBlockBuilders = updateBuilders;
			return new OutOfRangeBlock(outOfRange);
		}

		private IEnumerable<UpdateObjectDto> GetObjectsForUpdate() {
			var items = _player.Inventory.Select(x => new UpdateObjectDto(x));
			var seenObjects = ObjectManager.GetSeenObjectsNear(_player).Select(x => new UpdateObjectDto(x));
			return items.Concat(seenObjects);
		}

		private UpdateBlockBuilder GetBuilder(UpdateObjectDto obj) {
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

		#region Nested type: UpdateTimer

		private class UpdateTimer : Timer2 {
			private readonly UpdateManager _manager;

			public UpdateTimer(UpdateManager manager)
				: base(1000) {
				_manager = manager;
			}

			public override void OnTick() {
				_manager.UpdateObjects();
			}
		}

		#endregion
	}
}