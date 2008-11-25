using Hazzik.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	/// <summary>
	///This is a test class for PacketsHelperTest and is intended
	///to contain all PacketsHelperTest Unit Tests
	///</summary>
	[TestClass()]
	public class PacketsHelperTest {
		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext {
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		#region Additional test attributes

		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//

		#endregion

		/// <summary>
		///A test for GetBytes
		///</summary>
		[TestMethod()]
		public void GetBytesTest() {
			string pkt = @"0000: 01 00 00 00 00 00 F7 D4 F4 45 C4 0E 0C F0 08 00 : .........E......";

			var expected = new byte[] {
				0x01, 0x00, 0x00, 0x00,
				0x00, 0x00, 0xF7, 0xD4,
				0xF4, 0x45, 0xC4, 0x0E,
				0x0C, 0xF0, 0x08, 0x00
			};
			var actual = PacketsHelper.GetBytes(pkt);
			ArrayAssert.AreEqual(expected, actual);
		}
	}
}