using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Hazzik.Objects {
	public abstract partial class WorldObject {
		[StructLayout(LayoutKind.Explicit)]
		public struct UpdateValue {
			[FieldOffset(0)]
			public byte Uint8_0;
			[FieldOffset(1)]
			public byte Uint8_1;
			[FieldOffset(2)]
			public byte Uint8_2;
			[FieldOffset(3)]
			public byte Uint8_3;
			
			[FieldOffset(0)]
			public ushort UInt16_1;
			[FieldOffset(2)]
			public ushort UInt16_0;

			[FieldOffset(0)]
			public uint UInt32;

			[FieldOffset(0)]
			public float Single;
		}
		private readonly UpdateValue[] _values;

		protected WorldObject(int maxValues, uint type) {
			_values = new UpdateValue[maxValues];
			Guid = 1;
			ScaleX = 1f;
			Type = type;
		}

		public int MaxValues { get { return _values.Length; } }

		protected internal UpdateValue[] UpdateValues { get { return _values; } }

		public abstract byte TypeId { get; }

		public virtual byte UpdateFlag { get { return (byte)(UpdateFlags.HighGuid | UpdateFlags.LowGuid); } }

		#region GetValue

		protected internal uint GetValueUInt32(int index) {
			return _values[index].UInt32;
		}

		protected internal int GetValueInt32(int index) {
			return (int)GetValueUInt32(index);
		}

		protected internal ulong GetValueUInt64(int index) {
			return (ulong)GetValueUInt32(index + 1) << 32 | GetValueUInt32(index);
		}

		protected internal long GetValueInt64(int index) {
			return (long)GetValueUInt64(index);
		}

		protected internal float GetValueSingle(int index) {
			return _values[index].Single;
		}

		protected internal byte GetValueByte(int index, int index2) {
			var value = _values[index];
			switch(index2) {
			case 0:
				return value.Uint8_0;
			case 1:
				return value.Uint8_1;
			case 2:
				return value.Uint8_2;
			case 3:
				return value.Uint8_3;
			default:
				throw new ArgumentException("index2");
			}
		}

		protected internal ushort GetValueUInt16(int index, int index2) {
			var value = _values[index];
			switch(index2) {
			case 0:
				return value.UInt16_0;
			case 1:
				return value.UInt16_1;
			default:
				throw new ArgumentException("index2");
			}
		}

		protected internal short GetValueInt16(int index,int index2) {
			return (short)GetValueUInt16(index, index2);
		}

		#endregion

		#region SetValue

		protected internal void SetValue(int index, uint value) {
			_values[index].UInt32 = value;
		}

		protected internal void SetValue(int index, int value) {
			SetValue(index, (uint)value);
		}

		protected internal void SetValue(int index, ulong value) {
			SetValue(index, (uint)value);
			SetValue(index + 1, (uint)(value >> 32));
		}

		protected internal void SetValue(int index, long value) {
			SetValue(index, (ulong)value);
		}

		protected internal void SetValue(int index, float value) {
			_values[index].Single = value;
		}

		protected internal void SetValue(int index, int index2, byte value) {
			var updateValue = _values[index];
			switch(index2) {
			case 0:
				updateValue.Uint8_0 = value;
				break;
			case 1:
				updateValue.Uint8_1 = value;
				break;
			case 2:
				updateValue.Uint8_2 = value;
				break;
			case 3:
				updateValue.Uint8_3 = value;
				break;
			default:
				throw new ArgumentException("index2");
			}
			_values[index] = updateValue;
		}

		protected internal void SetValue(int index, int index2, ushort value) {
			var updateValue = _values[index];
			switch(index2) {
			case 0:
				updateValue.UInt16_0 = value;
				break;
			case 1:
				updateValue.UInt16_1 = value;
				break;
			default:
				throw new ArgumentException("index2");
			}
			_values[index] = updateValue;
		}

		protected internal void SetValue(int index, int index2, short value) {
			SetValue(index, index2, (ushort)value);
		}

		#endregion

		public virtual void WriteCreateBlock(BinaryWriter writer) {

		}
	}
}