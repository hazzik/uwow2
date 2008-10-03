using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Hazzik.Objects {
	#region UpdateType

	public enum UpdateType : byte {
		Values = 0,
		Movement = 1,
		CreateObject = 2,
		CreateObject2 = 3,
		OutOfRangeObjects = 4,
		NearObjects = 5,
	}

	#endregion

	#region UpdateFlags

	[Flags]
	public enum UpdateFlags {
		None = 0x00,
		Self = 0x01,
		Transport = 0x02,
		TargetGuid = 0x04,
		LowGuid = 0x08,
		HighGuid = 0x10,
		Mobile = 0x20,
		HasPosition = 0x40,
		Unknown1 = 0x80
	}

	#endregion

	#region ObjectTypes

	public enum ObjectTypeId : byte {
		Object = 0,
		Item = 1,
		Container = 2,
		Unit = 3,
		Player = 4,
		GameObject = 5,
		DynamicObject = 6,
		Corpse = 7
	}

	#endregion

	public abstract class WorldObject {

		private BitArray _updateMask;
		private UpdateFieldValue[] _updateValues;

		protected WorldObject(int updateMaskLength, uint type) {
			_updateMask = new BitArray(updateMaskLength);
			_updateValues = new UpdateFieldValue[updateMaskLength];

			this.Guid = ObjectGuid.NewGuid();
			this.Type = type;
			this.ScaleX = 1f;
		}

		public void ClearUpdateMask() {
			_updateMask.SetAll(false);
		}

		public abstract byte TypeId { get; }

		public virtual byte UpdateFlag {
			get { return (byte)(UpdateFlags.HighGuid | UpdateFlags.LowGuid); }
		}

		#region UpdateFields

		//OBJECT_FIELD_GUID = 0, // 2 4 1
		public virtual ulong Guid {
			get { return (ulong)GetUpdateFieldUInt32(UpdateFields.OBJECT_FIELD_GUID) << 32 | (ulong)GetUpdateFieldUInt32(UpdateFields.OBJECT_FIELD_GUID + 1); }
			set {
				SetUpdateField(UpdateFields.OBJECT_FIELD_GUID, (uint)(value >> 32));
				SetUpdateField(UpdateFields.OBJECT_FIELD_GUID + 1, (uint)(value));
			}
		}

		//OBJECT_FIELD_TYPE = 2, // 1 1 1
		public virtual uint Type {
			get { return GetUpdateFieldUInt32(UpdateFields.OBJECT_FIELD_TYPE); }
			set { SetUpdateField(UpdateFields.OBJECT_FIELD_TYPE, value); }
		}

		//OBJECT_FIELD_ENTRY = 3, // 1 1 1
		public virtual uint Entry {
			get { return GetUpdateFieldUInt32(UpdateFields.OBJECT_FIELD_ENTRY); }
			set { SetUpdateField(UpdateFields.OBJECT_FIELD_ENTRY, value); }
		}

		//OBJECT_FIELD_SCALE_X = 4, // 1 3 1
		public virtual float ScaleX {
			get { return GetUpdateFieldSingle(UpdateFields.OBJECT_FIELD_SCALE_X); }
			set { SetUpdateField(UpdateFields.OBJECT_FIELD_SCALE_X, value); }
		}

		//OBJECT_FIELD_PADDING = 5, // 1 1 0
		public virtual uint Pad {
			get { return GetUpdateFieldUInt32(UpdateFields.OBJECT_FIELD_PADDING); }
			set { SetUpdateField(UpdateFields.OBJECT_FIELD_PADDING, value); }
		}

		public void SetUpdateField(UpdateFields field, uint value) {
			_updateMask[(int)field] = true;
			_updateValues[(int)field].UInt32 = value;
		}

		public void SetUpdateField(UpdateFields field, float value) {
			_updateMask[(int)field] = true;
			_updateValues[(int)field].Single = value;
		}

		public uint GetUpdateFieldUInt32(UpdateFields field) {
			return _updateValues[(int)field].UInt32;
		}

		public float GetUpdateFieldSingle(UpdateFields field) {
			return _updateValues[(int)field].Single;
		}

		#endregion

		public virtual void WriteUpdateBlock(BinaryWriter w) {
			var dwordLength = _updateMask.Length / 32 + (_updateMask.Length % 32 != 0 ? 1 : 0);
			var buff = new byte[dwordLength * 4];
			_updateMask.CopyTo(buff, 0);
			w.Write((byte)dwordLength);
			w.Write(buff);
			for(int i = 0; i < _updateMask.Length; i++) {
				if(_updateMask[i]) {
					w.Write(_updateValues[i].UInt32);
				}
			}
		}

		public virtual void WriteCreateBlock(BinaryWriter w) {
		}
	}
}
