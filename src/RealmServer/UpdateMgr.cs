using System;
using System.Collections.Generic;
using System.IO;
using Hazzik.Objects;

namespace Hazzik {
	public class UpdateMgr {
		private List<WorldObject> _objects = new List<WorldObject>();

		public byte[] BuildUpdatePacket(Player to) {
			var s = new MemoryStream();
			var w = new BinaryWriter(s);
			w.Write(_objects.Count); //кол-во объектов
			w.Write((byte)0); //неизвестное значение, всегда 0
			foreach(var obj in _objects) {
				if(to.IsKnown(obj)) {
					WriteUpdateObject(w, obj);
				}
				else {
					WriteCreateObject(w, obj, to);
				}
			}
			w.Flush();
			return s.ToArray();
		}

		private void WriteCreateObject(BinaryWriter w, WorldObject obj, Player to) {
			w.Write((byte)(obj != to ? UpdateType.CreateObject : UpdateType.CreateObject2));
			w.WritePackGuid(obj.Guid);
			w.Write(obj.TypeId);
			w.Write((byte)(obj != to ? obj.UpdateFlag : obj.UpdateFlag | (byte)UpdateFlags.Self));
			obj.WriteCreateBlock(w);

			w.Write((uint)0);
			w.Write((uint)0);

			obj.WriteUpdateBlock(w);
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