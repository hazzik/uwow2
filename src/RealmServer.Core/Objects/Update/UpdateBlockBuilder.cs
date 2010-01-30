using System;
using System.Collections;
using Hazzik.Objects.Update.Blocks;

namespace Hazzik.Objects.Update {
	internal class UpdateBlockBuilder {
		private readonly WorldObject worldObject;
		private readonly Player player;
		private readonly UpdateValuesDto sendedDto;
		private bool isNew = true;

		public UpdateBlockBuilder(Player player, WorldObject obj) {
			this.player = player;
			worldObject = obj;
			sendedDto = new UpdateValuesDto(GetMaxValues(worldObject.TypeId), CreateRequiredMask());
		}

		private BitArray CreateRequiredMask() {
			var mask = new BitArray(GetMaxValues(worldObject.TypeId), true);
			for(int i = 0; i < mask.Length; i++) {
                if (worldObject.TypeId == ObjectTypeId.Player && player != worldObject && i > (int)(UpdateFields.PLAYER_FIELD_PAD_0))
                {
					mask[i] = false;
				}
			}
			return mask;
		}

		public IUpdateBlock CreateUpdateBlock() {
			UpdateObjectDtoMapper.Update(sendedDto, worldObject);
			if(isNew) {
				isNew = false;
				return new CreateBlockWriter(worldObject.Guid == player.Guid, worldObject, sendedDto);
			}
			return new UpdateBlockWriter(worldObject.Guid, sendedDto);
		}

		private static int GetMaxValues(ObjectTypeId typeId) {
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