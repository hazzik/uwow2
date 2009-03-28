using System;
using System.Collections;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	internal class UpdateBlockBuilder {
		private readonly Player _player;
		public UpdateObjectDto Obj { get; set; }
		private readonly BitArray _required;
		private readonly uint[] _sendedValues;
		private bool _isNew = true;

		public UpdateBlockBuilder(Player player, UpdateObjectDto obj) {
			_player = player;
			Obj = obj;
			_required = GetRequiredMask();
			_sendedValues = new uint[Obj.MaxValues];
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
				return new CreateBlock(Obj.Guid == _player.Guid, Obj, mask, (uint[])_sendedValues.Clone());
			}
			return new UpdateBlock(Obj, mask, _sendedValues);
		}

		private BitArray BuildMask(int maskLength) {
			var mask = new BitArray(maskLength);
			for(int i = 0; i < mask.Length; i++) {
				mask[i] = _required[i] && GetNewValue(i);
			}
			return mask;
		}

		private bool GetNewValue(int i) {
			uint newValue = Obj.GetValue(i);
			uint oldValue = _sendedValues[i];
			_sendedValues[i] = newValue;
			return oldValue != newValue;
		}
	}
}