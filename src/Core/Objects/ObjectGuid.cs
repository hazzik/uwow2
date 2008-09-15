using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik {
	public class ObjectGuid {
		private static ulong _lastGuid = 0;
		public static ulong NewGuid() {
			return ++_lastGuid;
		}
	}
}
