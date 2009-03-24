using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

		public class ObjectUpdater : IUpdateBlock {
			private readonly WorldObject _obj;
			private readonly BitArray _required;
			private readonly Player _to;
			private bool _changed;
			private bool _isNew = true;
			private BitArray _mask;
			private uint[] _sendedValues;

			public ObjectUpdater(Player to, WorldObject obj) {
				_to = to;
				_obj = obj;
				_required = to.GetRequeredMask(_obj);
				_sendedValues = new uint[_obj.MaxValues];
			}

			#region IUpdateBlock Members

			public bool IsEmpty {
				get {
					if(!_changed) {
						_mask = BuildMask(out _changed);
					}
					return !_changed;
				}
			}

			public void Write(BinaryWriter writer) {
				writer.WritePackGuid(_obj.Guid);
				if(_isNew) {
					writer.Write((byte)_obj.TypeId);
					writer.Write((byte)(_obj != _to ? _obj.UpdateFlag : _obj.UpdateFlag | UpdateFlags.Self));
					_obj.WriteCreateBlock(writer);
					if(_obj.UpdateFlag.Has(UpdateFlags.HighGuid)) {
						writer.Write((uint)0x00);
					}
					if(_obj.UpdateFlag.Has(UpdateFlags.LowGuid)) {
						writer.Write((uint)0x00);
					}
					if(_obj.UpdateFlag.Has(UpdateFlags.TargetGuid)) {
						writer.WritePackGuid(0x00);
					}
					if(_obj.UpdateFlag.Has(UpdateFlags.Transport)) {
						writer.Write((uint)0x00);
					}
					_isNew = false;
				}
				WriteMask(writer);
				for(int i = 0; i < _mask.Length; i++) {
					if(_mask[i]) {
						writer.Write(_sendedValues[i]);
					}
				}
				_changed = false;
			}

			public UpdateType UpdateType {
				get { return !_isNew ? UpdateType.Values : (_obj != _to ? UpdateType.CreateObject : UpdateType.CreateObject2); }
			}

			#endregion

			private void WriteMask(BinaryWriter writer) {
				var length = (byte)GetLengthInDwords(_mask.Length);
				var buffer = new byte[length << 2];
				_mask.CopyTo(buffer, 0);
				writer.Write(length);
				writer.Write(buffer);
			}

			private BitArray BuildMask(out bool changed) {
				changed = false;
				var values = new uint[_obj.MaxValues];
				var mask = new BitArray(Math.Min(_required.Length, values.Length));
				for(int i = 0; i < mask.Length; i++) {
					changed |= mask[i] = _required[i] && _sendedValues[i] != (values[i] = _obj.GetValue(i));
				}
				_sendedValues = values;
				return mask;
			}

			private static int GetLengthInDwords(int bitsCount) {
				return (bitsCount >> 5) + (bitsCount % 32 != 0 ? 1 : 0);
			}

			public IUpdateBlock CreateUpdateBlock() {
				return this;
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