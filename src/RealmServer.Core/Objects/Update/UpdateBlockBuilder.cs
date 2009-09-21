using System;
using System.Collections;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	internal class UpdateBlockBuilder {
		private readonly WorldObject _obj;
		private readonly Player _player;
		private readonly UpdateValuesDto _sendedDto;
		private bool _isNew = true;

		public UpdateBlockBuilder(Player player, WorldObject obj) {
			_player = player;
			_obj = obj;
			_sendedDto = new UpdateValuesDto(GetMaxValues(_obj.TypeId), CreateRequiredMask());
		}

		private BitArray CreateRequiredMask() {
			var mask = new BitArray(GetMaxValues(_obj.TypeId), true);
			for(int i = 0; i < mask.Length; i++) {
				if(_obj.TypeId == ObjectTypeId.Player && _player != _obj && i > (int)(UpdateFields.PLAYER_FIELD_INV_SLOT_HEAD - 1)) {
					mask[i] = false;
				}
			}
			return mask;
		}

		public IUpdateBlock CreateUpdateBlock() {
			UpdateObjectDtoMapper.Update(_sendedDto, _obj);
			if(_isNew) {
				_isNew = false;
				return new CreateBlockWriter(_obj.Guid == _player.Guid, _obj, _sendedDto);
			}
			return new UpdateBlockWriter(_obj.Guid, _sendedDto);
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