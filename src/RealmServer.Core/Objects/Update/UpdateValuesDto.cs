using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace Hazzik.Objects.Update {
	internal class UpdateValuesDto {
		private readonly BitArray required;
		private readonly BitArray updateMask;
		private readonly UpdateValue[] values;

		public UpdateValuesDto(int valuesCount, BitArray required) {
			values = new UpdateValue[valuesCount];
			updateMask = new BitArray(valuesCount);
			this.required = required;
		}

		#region Set

		private void Set(UpdateFields field, UpdateValue value) {
			var index = (int)field;
			updateMask[index] = required[index] & !Equals(values[index], value);
			values[index] = value;
		}

		protected internal void Set(UpdateFields field, uint value) {
			Set(field, new UpdateValue { UInt32 = value });
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
			Set(field, new UpdateValue { Single = value });
		}

		protected internal void Set(UpdateFields field, ushort value0, ushort value1) {
			UpdateValue updateValue = values[(int)field];
			updateValue.UInt16_0 = value0;
			updateValue.UInt16_1 = value1;
			Set(field, updateValue);
		}

		protected internal void Set(UpdateFields field, short value0, short value1) {
			Set(field, (ushort)value0, (ushort)value1);
		}

		protected internal void Set(UpdateFields field, byte byte0, byte byte1, byte byte2, byte byte3) {
			UpdateValue updateValue = values[(int)field];
			updateValue.Uint8_0 = byte0;
			updateValue.Uint8_1 = byte1;
			updateValue.Uint8_2 = byte2;
			updateValue.Uint8_3 = byte3;
			Set(field, updateValue);
		}

		#endregion

		public bool Dirty {
            get { return updateMask.Cast<bool>().Any(b => b); }
		}

	    public void Write(BinaryWriter writer) {
			var length = (byte)GetLengthInDwords(updateMask.Length);
			var buffer = new byte[length << 2];
			updateMask.CopyTo(buffer, 0);
			writer.Write(length);
			writer.Write(buffer);
			for(int i = 0; i < updateMask.Length; i++) {
				if(updateMask[i]) {
					writer.Write(values[i].UInt32);
				}
			}
		}

		private static int GetLengthInDwords(int bitsCount) {
			return (bitsCount >> 5) + (bitsCount % 32 != 0 ? 1 : 0);
		}
	}
}