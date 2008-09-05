using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzik {
	public class ObjectGuid {
		private static long _lastGuid = 1;
		public static long NewGuid() {
			return ++_lastGuid;
		}
	}
}
