using System;
using System.Collections;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	public class ObjectUpdater : IUpdateBuilder {
		private readonly WorldObject _obj;
		private readonly BitArray _required;
		private readonly Player _to;
		private bool _isNew = true;
		private BitArray _mask;
		private uint[] _sendedValues;

		public ObjectUpdater(Player to, WorldObject obj) {
			_to = to;
			_obj = obj;
			_required = to.GetRequeredMask(_obj);
			_sendedValues = new uint[_obj.MaxValues];
		}

		#region IUpdateBuilder Members

		public bool IsChanged {
			get {
				bool changed;
				_mask = BuildMask(out changed);
				return changed;
			}
		}

		public void Write(BinaryWriter writer) {
			writer.Write((byte)GetUpdateType());
			writer.WritePackGuid(_obj.Guid);
			if(_isNew) {
				writer.Write(_obj.TypeId);
				writer.Write((byte)(_obj != _to ? _obj.UpdateFlag : _obj.UpdateFlag | (byte)UpdateFlags.Self));
				_obj.WriteCreateBlock(writer);
				writer.Write((uint)0);
				writer.Write((uint)0);
				_isNew = false;
			}
			WriteMask(writer);
			for(int i = 0; i < _mask.Length; i++) {
				if(_mask[i]) {
					writer.Write(_sendedValues[i]);
				}
			}
		}

		#endregion

		private void WriteMask(BinaryWriter writer) {
			var length = (byte)GetLengthInDwords(_mask.Length);
			var buffer = new byte[length << 2];
			_mask.CopyTo(buffer, 0);
			writer.Write(length);
			writer.Write(buffer);
		}

		private UpdateType GetUpdateType() {
			return !_isNew ? UpdateType.Values : (_obj != _to ? UpdateType.CreateObject : UpdateType.CreateObject2);
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
	}
}