using System;

namespace Hazzik.Objects {
	public static class WorldObjectExtensions {
		public static ulong EntityId(this WorldObject source) {
			return (null != source) ? source.Guid : 0;
		}
	}
}