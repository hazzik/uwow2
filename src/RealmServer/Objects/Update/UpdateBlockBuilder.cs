using System;
using System.Collections;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	internal class UpdateBlockBuilder {
		private readonly Player _player;
		private readonly BitArray _required;
		private readonly uint[] _sendedValues;
		private bool _isNew = true;
		private readonly WorldObject _obj;

		public UpdateBlockBuilder(Player player, WorldObject obj) {
			_player = player;
			_obj = obj;
			_required = GetRequiredMask();
			_sendedValues = new uint[GetMaxValues(obj.TypeId)];
		}

		private static BitArray GetRequiredMask() {
			var mask = new BitArray((int)UpdateFields.PLAYER_END);
			mask.SetAll(true);
			return mask;
		}

		public IUpdateBlock CreateUpdateBlock() {
			return CreateUpdateBlock(UpdateObjectDtoMapper.CreateDto(_obj));
		}

		private IUpdateBlock CreateUpdateBlock(UpdateValuesDto dto) {
			var maskLength = Math.Min(_required.Length, _sendedValues.Length);

			var mask = BuildMask(dto, maskLength);
			var updateBlock = new UpdateBlock(mask, (uint[])_sendedValues.Clone());
			if(_isNew) {
				_isNew = false;
				return new CreateBlockWriter(_obj.Guid == _player.Guid, _obj, updateBlock);
			}
			return new UpdateBlockWriter(_obj.Guid, updateBlock);
		}

		private BitArray BuildMask(UpdateValuesDto dto, int maskLength) {
			var mask = new BitArray(maskLength);
			for(int i = 0; i < mask.Length; i++) {
				mask[i] = _required[i] && GetNewValue(dto, i);
			}
			return mask;
		}

		private bool GetNewValue(UpdateValuesDto dto, int index) {
			uint newValue = dto.GetValue(index);
			uint oldValue = _sendedValues[index];
			_sendedValues[index] = newValue;
			return oldValue != newValue;
		}

		public static int GetMaxValues(ObjectTypeId typeId) {
			switch(typeId) {
			case ObjectTypeId.Object:
				return (int)UpdateFields.OBJECT_END;
			case ObjectTypeId.Item:
				return (int)UpdateFields.ITEM_END;
			case ObjectTypeId.Container:
				return (int)UpdateFields.CONTAINER_END;
			case ObjectTypeId.Unit:
				return (int)UpdateFields.UNIT_END;
			case ObjectTypeId.Player:
				return (int)UpdateFields.PLAYER_END;
			case ObjectTypeId.GameObject:
				return (int)UpdateFields.GAMEOBJECT_END;
			case ObjectTypeId.DynamicObject:
				return (int)UpdateFields.DYNAMICOBJECT_END;
			case ObjectTypeId.Corpse:
				return (int)UpdateFields.CORPSE_END;
			case ObjectTypeId.AIGroup:
				return (int)UpdateFields.OBJECT_END;
			case ObjectTypeId.AreaTrigger:
				return (int)UpdateFields.OBJECT_END;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}