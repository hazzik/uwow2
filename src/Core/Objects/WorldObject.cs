using System;
using System.IO;

namespace Hazzik.Objects {
	public abstract partial class WorldObject {
		private readonly uint[] _values;

		protected WorldObject(int maxValues, uint type) {
			_values = new uint[maxValues];
			Guid = 1;
			ScaleX = 1f;
			Type = type;
		}

		public int MaxValues { get { return _values.Length; } }

		protected internal uint[] UpdateValues { get { return _values; } }

		public abstract byte TypeId { get; }

		public virtual byte UpdateFlag { get { return (byte)(UpdateFlags.HighGuid | UpdateFlags.LowGuid); } }

		#region GetValue

		protected uint GetValueUInt32(int index) {
			return _values[index];
		}

		protected int GetValueInt32(int index) {
			return (int)GetValueUInt32(index);
		}

		protected ulong GetValueUInt64(int index) {
			return (ulong)GetValueInt32(index + 1) << 32 | (ulong)GetValueInt32(index);
		}

		protected long GetValueInt64(int index) {
			return (long)GetValueUInt64(index);
		}

		protected float GetValueSingle(int index) {
			return BitConverter.ToSingle(BitConverter.GetBytes(GetValueUInt32(index)), 0);
		}

		#endregion

		#region SetValue

		protected void SetValue(int index, uint value) {
			_values[index] = value;
		}

		protected void SetValue(int index, int value) {
			SetValue(index, (uint)value);
		}

		protected void SetValue(int index, ulong value) {
			SetValue(index, (uint)value);
			SetValue(index + 1, (uint)(value >> 32));
		}

		protected void SetValue(int index, long value) {
			SetValue(index, (ulong)value);
		}

		protected void SetValue(int index, float value) {
			SetValue(index, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
		}

		#endregion

		public virtual void WriteCreateBlock(BinaryWriter writer) {

		}
	}
}