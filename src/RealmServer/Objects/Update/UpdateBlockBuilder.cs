using System;
using System.Collections;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	internal class UpdateBlockBuilder {
		private readonly Player _player;
		private readonly WorldObject _obj;
		private readonly BitArray _required;
		private readonly uint[] _sendedValues;
		private bool _isNew = true;

		public UpdateBlockBuilder(Player player, WorldObject obj) {
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
}