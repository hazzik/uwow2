using System;
using System.Linq;

namespace Hazzik {
	public static class ByteArrayExtensions
	{
		public static byte[] Reverse(this byte[] from)
		{
			var res = new byte[from.Length];
			int i = 0;
			for (int t = from.Length - 1; t >= 0; t--)
			{
				res[i++] = from[t];
			}
			return res;
		}

		public static bool Equals(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			return !left.Where((t, i) => right[i] != t).Any();
		}
	}
	public static class Utility {
		public static readonly Random Seed = new Random();
	}
}