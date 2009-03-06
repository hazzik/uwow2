using Hazzik.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	[TestClass]
	public class PacketsHelperTest {
		[TestMethod]
		public void GetBytesTest() {
			string pkt = @"0000: 01 00 00 00 00 00 F7 D4 F4 45 C4 0E 0C F0 08 00 : .........E......";

			var expected = new byte[] {
				0x01, 0x00, 0x00, 0x00,
				0x00, 0x00, 0xF7, 0xD4,
				0xF4, 0x45, 0xC4, 0x0E,
				0x0C, 0xF0, 0x08, 0x00
			};
			byte[] actual = PacketsHelper.GetBytes(pkt);
			ArrayAssert.AreEqual(expected, actual);
		}
	}
}