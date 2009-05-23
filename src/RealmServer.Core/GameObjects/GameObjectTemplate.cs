using System;
using System.IO;
using Hazzik.Net;

namespace Hazzik.GameObjects {
	public class GameObjectTemplate {
		public GameObjectTemplate() {
			Fields = new uint[24];
		}

		public uint Id { get; set; }
		public GameObjectType Type { get; set; }
		public uint DisplayId { get; set; }
		public string Name { get; set; }
		public uint[] Fields { get; set; }
		public float ScaleX { get; set; }
	}
}