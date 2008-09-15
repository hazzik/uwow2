using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Hazzik.Objects {
	public abstract class WorldObject {

		private BitArray _updateMask;
		private uint[] _updateValues;

		public WorldObject(int updateMaskLength) {
			_updateMask = new BitArray(updateMaskLength);
			_updateValues = new uint[updateMaskLength];
		}

		public void ClearUpdateMask() {
			_updateMask.SetAll(false);
		}

		//OBJECT_FIELD_GUID = 0, // 2 4 1
		public virtual ulong Guid {
			get { return (ulong)_updateValues[0]<< 32 | (ulong)_updateValues[1] ; }
			set {
				UpdateValue(UpdateFields.OBJECT_FIELD_GUID, (uint)(value >> 32));
				UpdateValue(UpdateFields.OBJECT_FIELD_GUID + 1, (uint)(value));
			}
		}
		//OBJECT_FIELD_TYPE = 2, // 1 1 1
		public virtual int Type { get; set; }
		//OBJECT_FIELD_ENTRY = 3, // 1 1 1
		public virtual int Entry { get; set; }
		//OBJECT_FIELD_SCALE_X = 4, // 1 3 1
		public virtual float ScaleX { get; set; }
		//OBJECT_FIELD_PADDING = 5, // 1 1 0
		public virtual int Pad { get { return 0; } }

		public void UpdateValue(UpdateFields field, uint value) {
			_updateMask[(int)field] = true;
			_updateValues[(int)field] = value;
		}

		public abstract void Accept(IObjectVisitor visitor);

		public void WriteUpdateBlock(BinaryWriter w) {
			var dwordLength = _updateMask.Length / 32 + (_updateMask.Length % 32 != 0 ? 1 : 0);
			var buff = new byte[dwordLength * 4];
			_updateMask.CopyTo(buff, 0);
			w.Write((byte)dwordLength);
			w.Write(buff);
			for(int i = 0; i < _updateMask.Length; i++) {
				if(_updateMask[i]) {
					w.Write(_updateValues[i]);
				}
			}
		}
	}
}
