using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hazzik.Objects;
using System.IO;
using System.Collections;

namespace Hazzik {
	public class UpdateMgr {
		private List<WorldObject> _objects = new List<WorldObject>();

		public byte[] BuildCreateObject(WorldObject obj) {
			return null;
		}

		public byte[] BuildUpdatePacket() {
			var s = new MemoryStream();
			var w = new BinaryWriter(s);
			w.Write(_objects.Count); //кол-во объектов
			w.Write((byte)0); //неизвестное значение, всегда 0
			foreach(var obj in _objects) {
				WriteUpdateObject(w, obj);
			}
			w.Flush();
			return s.ToArray();
		}

		private void WriteUpdateObject(BinaryWriter w, WorldObject obj) {
			w.Write((byte)0); //
			w.WritePackGuid(obj.Guid);
			obj.WriteUpdateBlock(w);
		}

		public void Add(Unit obj) {
			_objects.Add(obj);
		}
	}
}
