using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.Objects {
	public abstract partial class WorldObject {
		private readonly UpdateValue[] _values;

		protected WorldObject(int maxValues) {
			_values = new UpdateValue[maxValues];
			Guid = ObjectGuid.NewGuid();
			Type = ObjectTypes.Object;
			ScaleX = 1f;
		}

		public int MaxValues { get { return _values.Length; } }

		public abstract ObjectTypeId TypeId { get; }

		public virtual UpdateFlags UpdateFlag {
			get { return UpdateFlags.HighGuid | UpdateFlags.LowGuid; }
		}

		#region GetValue

		protected internal uint GetValue(int field) {
			return GetUInt32((UpdateFields)field);
		}

		protected internal uint GetUInt32(UpdateFields field) {
			return _values[(int)field].UInt32;
		}

		protected internal int GetInt32(UpdateFields field) {
			return (int)GetUInt32(field);
		}

		protected internal ulong GetUInt64(UpdateFields field) {
			return (ulong)GetUInt32(field + 1) << 32 | GetUInt32(field);
		}

		protected internal long GetInt64(UpdateFields field) {
			return (long)GetUInt64(field);
		}

		protected internal float GetSingle(UpdateFields field) {
			return _values[(int)field].Single;
		}

		protected internal byte GetByte(UpdateFields field, int index) {
			var value = _values[(int)field];
			switch(index) {
			case 0:
				return value.Uint8_0;
			case 1:
				return value.Uint8_1;
			case 2:
				return value.Uint8_2;
			case 3:
				return value.Uint8_3;
			default:
				throw new ArgumentException("index");
			}
		}

		protected internal ushort GetUInt16(UpdateFields field, int index) {
			var value = _values[(int)field];
			switch(index) {
			case 0:
				return value.UInt16_0;
			case 1:
				return value.UInt16_1;
			default:
				throw new ArgumentException("index");
			}
		}

		protected internal short GetInt16(UpdateFields field, int index) {
			return (short)GetUInt16(field, index);
		}

		#endregion

		#region SetValue

		protected internal void SetUInt32(UpdateFields field, uint value) {
			_values[(int)field].UInt32 = value;
		}

		protected internal void SetInt32(UpdateFields field, int value) {
			SetUInt32(field, (uint)value);
		}

		protected internal void SetUInt64(UpdateFields field, ulong value) {
			SetUInt32(field, (uint)value);
			SetUInt32(field + 1, (uint)(value >> 32));
		}

		protected internal void SetInt64(UpdateFields field, long value) {
			SetUInt64(field, (ulong)value);
		}

		protected internal void SetSingle(UpdateFields field, float value) {
			_values[(int)field].Single = value;
		}

		protected internal void SetByte(UpdateFields field, int index, byte value) {
			var updateValue = _values[(int)field];
			switch(index) {
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
				throw new ArgumentException("index");
			}
			_values[(int)field] = updateValue;
		}

		protected internal void SetUInt16(UpdateFields field, int index, ushort value) {
			var updateValue = _values[(int)field];
			switch(index) {
			case 0:
				updateValue.UInt16_0 = value;
				break;
			case 1:
				updateValue.UInt16_1 = value;
				break;
			default:
				throw new ArgumentException("index");
			}
			_values[(int)field] = updateValue;
		}

		protected internal void SetInt16(UpdateFields field, int index, short value) {
			SetUInt16(field, index, (ushort)value);
		}

		#endregion

		public virtual void WriteCreateBlock(BinaryWriter writer) {

		}

		public IPacket GetDestroyObjectPkt() {
			var result = new WorldPacket(WMSG.SMSG_DESTROY_OBJECT);
			var writer = result.CreateWriter();
			writer.Write(Guid);
			return result;
		}
	}
}