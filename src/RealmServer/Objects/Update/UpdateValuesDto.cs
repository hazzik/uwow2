using System;

namespace Hazzik.Objects.Update {
	public class UpdateValuesDto {
		private readonly WorldObject _obj;
		private readonly UpdateValue[] _values;

		public UpdateValuesDto(WorldObject obj) {
			_obj = obj;
			_values = new UpdateValue[GetMaxValues(_obj.TypeId)];
			UpdateObjectDtoMapper.Update(this, obj);
		}

		public int MaxValues {
			get { return _values.Length; }
		}

		public virtual ulong Guid {
			get { return _obj.Guid; }
		}

		public WorldObject Object {
			get { return _obj; }
		}

		#region Send/Update

		protected internal uint GetValue(int field) {
		   return _values[(int)((UpdateFields)field)].UInt32;
		}

		protected internal void Set(UpdateFields field, uint value) {
			_values[(int)field].UInt32 = value;
		}

		protected internal void Set(UpdateFields field, int value) {
			Set(field, (uint)value);
		}

		protected internal void Set(UpdateFields field, ulong value) {
			Set(field, (uint)value);
			Set(field + 1, (uint)(value >> 32));
		}

		protected internal void Set(UpdateFields field, long value) {
			Set(field, (ulong)value);
		}

		protected internal void Set(UpdateFields field, float value) {
			_values[(int)field].Single = value;
		}

		protected internal void Set(UpdateFields field, ushort value0, ushort value1) {
			UpdateValue updateValue = _values[(int)field];
			updateValue.UInt16_0 = value0;
			updateValue.UInt16_1 = value1;
			_values[(int)field] = updateValue;
		}

		protected internal void Set(UpdateFields field, short value0, short value1) {
			Set(field, (ushort)value0, (ushort)value1);
		}
		
		protected internal void Set(UpdateFields field, byte byte0, byte byte1, byte byte2, byte byte3) {
			UpdateValue updateValue = _values[(int)field];
			updateValue.Uint8_0 = byte0;
			updateValue.Uint8_1 = byte1;
			updateValue.Uint8_2 = byte2;
			updateValue.Uint8_3 = byte3;
			_values[(int)field] = updateValue;
		}

		#endregion

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