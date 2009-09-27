using System;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Map;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	public class UpdateManager {
		private readonly Player player;
		private readonly Timer2 updateTimer;
		private IDictionary<ulong, UpdateBlockBuilder> updateBlockBuilders = new Dictionary<ulong, UpdateBlockBuilder>();

		public UpdateManager(Player player) {
			this.player = player;
			this.player.SetUpdateManager(this);
			updateTimer = new UpdateTimer(this);
		}

		public void UpdateObjects() {
			ICollection<IUpdateBlock> updateBlocks = GetUpdateBlocks();
			player.Session.SendUpdateObjects(new UpdatePacketBuilder(updateBlocks));
		}

		private ICollection<IUpdateBlock> GetUpdateBlocks() {
			IEnumerable<IUpdateBlock> allBlocks = new[] { GetOutOfRange() }.Concat(updateBlockBuilders.Values.Select(x => x.CreateUpdateBlock()));
			return allBlocks.Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			Dictionary<ulong, UpdateBlockBuilder> updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, x => GetBuilder(x));
			List<ulong> outOfRange = updateBlockBuilders.Keys.Except(updateBuilders.Keys).ToList();
			updateBlockBuilders = updateBuilders;
			return new OutOfRangeBlockWriter(outOfRange);
		}

		private IEnumerable<WorldObject> GetObjectsForUpdate() {
			IEnumerable<WorldObject> items = player.Inventory.Cast<WorldObject>();
			IEnumerable<WorldObject> seenObjects = ObjectManager.GetSeenObjectsNear(player).Cast<WorldObject>();
			return items.Concat(seenObjects);
		}

		private UpdateBlockBuilder GetBuilder(WorldObject obj) {
			UpdateBlockBuilder result;
			if(!updateBlockBuilders.TryGetValue(obj.Guid, out result)) {
				var updater = new UpdateBlockBuilder(player, obj);
				updateBlockBuilders[obj.Guid] = updater;
				return updater;
			}
			return result;
		}

		public void StartUpdateTimer() {
			updateTimer.Start();
		}

		public void StopUpdateTimer() {
			updateTimer.Stop();
		}
	}
}