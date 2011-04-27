using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hazzik.Map;
using Hazzik.Net;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	public class UpdateManager : IDisposable {
		private readonly object lockObject = new object();
		private readonly ISession session;
		private Timer timer;
		private IDictionary<ulong, UpdateBlockBuilder> updateBlockBuilders = new Dictionary<ulong, UpdateBlockBuilder>();

		public UpdateManager(ISession session) {
			this.session = session;
		    timer = new Timer(state => UpdateObjects(), null, 0, 1000);
		}

		#region IDisposable Members

		public void Dispose() {
			timer.Dispose();
			timer = null;
		}

		#endregion

		private void UpdateObjects() {
			lock(lockObject) {
                    session.Send(new UpdatePacketBuilder(GetUpdateBlocks()));
			}
		}

		private ICollection<IUpdateBlock> GetUpdateBlocks() {
			IEnumerable<IUpdateBlock> allBlocks = new[] { GetOutOfRange() }.Concat(updateBlockBuilders.Values.Select(x => x.CreateUpdateBlock()));
			return allBlocks.Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			Dictionary<ulong, UpdateBlockBuilder> updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, GetBuilder);
			List<ulong> outOfRange = updateBlockBuilders.Keys.Except(updateBuilders.Keys).ToList();
			updateBlockBuilders = updateBuilders;
			return new OutOfRangeBlockWriter(outOfRange);
		}

		private IEnumerable<WorldObject> GetObjectsForUpdate() {
			IEnumerable<WorldObject> items = session.Player.Inventory;
			IEnumerable<WorldObject> seenObjects = ObjectManager.GetSeenObjectsNear(session.Player);
			return items.Concat(seenObjects);
		}

		private UpdateBlockBuilder GetBuilder(WorldObject obj) {
			UpdateBlockBuilder result;
			if(!updateBlockBuilders.TryGetValue(obj.Guid, out result)) {
				var updater = new UpdateBlockBuilder(session.Player, obj);
				updateBlockBuilders[obj.Guid] = updater;
				return updater;
			}
			return result;
		}

		public void Start() {
			UpdateObjects();
			timer.Change(1000, 1000);
		}

		public void Stop() {
			timer.Change(Timeout.Infinite, Timeout.Infinite);
		}
	}
}