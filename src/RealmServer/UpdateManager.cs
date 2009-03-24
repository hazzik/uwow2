using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hazzik.Map;
using Hazzik.Objects;

namespace Hazzik {
	public class UpdateManager {
		private readonly Player _player;
		private readonly Timer2 _updateTimer;
		private IDictionary<ulong, ObjectUpdater> _objectUpdaters = new Dictionary<ulong, ObjectUpdater>();

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
			var allBlocks = new[] { GetOutOfRange() }.Concat(_objectUpdaters.Values.Select(x=>x.CreateUpdateBlock()));
			return allBlocks.Where(x => !x.IsEmpty).ToList();
		}

		private IUpdateBlock GetOutOfRange() {
			var updateBuilders = GetObjectsForUpdate().ToDictionary(x => x.Guid, x => GetUpdater(x));
			var outOfRange = _objectUpdaters.Keys.Except(updateBuilders.Keys).ToList();
			_objectUpdaters = updateBuilders;
			return new OutOfRangeBlock(outOfRange);
		}

		private IEnumerable<WorldObject> GetObjectsForUpdate() {
			var items = _player.Items.Where(x => x != null).Cast<WorldObject>();
			var seenObjects = ObjectManager.GetSeenObjectsNear(_player).Cast<WorldObject>();
			return items.Concat(seenObjects);
		}

		private ObjectUpdater GetUpdater(WorldObject obj) {
			ObjectUpdater result;
			if(!_objectUpdaters.TryGetValue(obj.Guid, out result)) {
				var updater = new ObjectUpdater(_player, obj);
				_objectUpdaters[obj.Guid] = updater;
				return updater;
			}
			return result;
		}

		public void StartUpdateTimer() {
			_updateTimer.Start();
		}

		#region Nested type: ObjectUpdater

		private class ObjectUpdater {
			private readonly Player _player;
			private readonly WorldObject _obj;
			private readonly BitArray _required;
			private readonly uint[] _sendedValues;
			private bool _isNew = true;

			public ObjectUpdater(Player player, WorldObject obj) {
				_player = player;
				_obj = obj;
				_required = GetRequiredMask();
				_sendedValues = new uint[_obj.MaxValues];
			}

			private static BitArray GetRequiredMask() {
				var mask = new BitArray((int)UpdateFields.PLAYER_END);
				mask.SetAll(true);
				return mask;
			}

			public IUpdateBlock CreateUpdateBlock() {
				var maskLength = Math.Min(_required.Length, _sendedValues.Length);
				var mask = BuildMask(maskLength);
				if(_isNew) {
					_isNew = false;
					return new CreateBlock(_obj.Guid == _player.Guid, _obj, mask, (uint[])_sendedValues.Clone());
				}
				return new UpdateBlock(_obj, mask, _sendedValues);
			}

			private BitArray BuildMask(int maskLength) {
				var mask = new BitArray(maskLength);
				for(int i = 0; i < mask.Length; i++) {
					mask[i] = _required[i] && GetNewValue(i);
				}
				return mask;
			}

			private bool GetNewValue(int i) {
				uint newValue = _obj.GetValue(i);
				uint oldValue = _sendedValues[i];
				_sendedValues[i] = newValue;
				return oldValue != newValue;
			}
		}

		#endregion

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