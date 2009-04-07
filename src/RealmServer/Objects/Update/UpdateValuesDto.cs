using System;

namespace Hazzik.Objects.Update {
	public class UpdateValuesDto {
		private readonly UpdateValue[] _values;

		public UpdateValuesDto(int valuesCount) {
			_values = new UpdateValue[valuesCount];
		}

		#region Send/Update

		protected internal uint GetValue(int field) {
		   return _values[(int)((UpdateFields)field)].UInt32;
		}

		protected internal void Set(UpdateFields field, uint value) {
			_values[(int)field] = new UpdateValue { UInt32 = value };
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
			_values[(int)field] = new UpdateValue { Single = value };
		}

		protected internal void Set(UpdateFields field, ushort value0, ushort value1) {
			var updateValue = _values[(int)field];
			updateValue.UInt16_0 = value0;
			updateValue.UInt16_1 = value1;
			_values[(int)field] = updateValue;
		}

		protected internal void Set(UpdateFields field, short value0, short value1) {
			Set(field, (ushort)value0, (ushort)value1);
		}
		
		protected internal void Set(UpdateFields field, byte byte0, byte byte1, byte byte2, byte byte3) {
			var updateValue = _values[(int)field];
			updateValue.Uint8_0 = byte0;
			updateValue.Uint8_1 = byte1;
			updateValue.Uint8_2 = byte2;
			updateValue.Uint8_3 = byte3;
			_values[(int)field] = updateValue;
		}

		#endregion
	}
}