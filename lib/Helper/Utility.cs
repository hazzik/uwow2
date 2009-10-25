using System;
using System.Linq;

namespace Hazzik {
	public static class Utility {
		public static readonly Random Seed = new Random();

		public static byte[] Reverse(this byte[] from) {
			var res = new byte[from.Length];
			int i = 0;
			for(int t = from.Length - 1; t >= 0; t--) {
				res[i++] = from[t];
			}
			return res;
		}

		public static byte[] Concat(this byte[] a, byte[] b) {
			var tmp = new byte[a.Length + b.Length];
			Buffer.BlockCopy(a, 0, tmp, 0, a.Length);
			Buffer.BlockCopy(b, 0, tmp, a.Length, b.Length);
			return tmp;
		}

		public static bool Equals(byte[] left, byte[] right) {
			if(left.Length != right.Length) {
				return false;
			}
			return !left.Where((t, i) => right[i] != t).Any();
		}
	}
}