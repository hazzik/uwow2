using Hazzik;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hazzik.Objects;
using System;
using Hazzik.Helper;

namespace Tests {

	/// <summary>
	///This is a test class for UpdateMgrTest and is intended
	///to contain all UpdateMgrTest Unit Tests
	///</summary>
	[TestClass()]
	public class UpdateMgrTest {
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
		///A test for BuildUpdateObject
		///</summary>
		[TestMethod()]
		public void BuildUpdateObjectTest1() {
			var target = new UpdateMgr();
			var expected = PacketsHelper.GetBytes(@"

0000: 01 00 00 00 00 00 F7 D4 F4 45 C4 0E 0C F0 08 00 : .........E......
0010: 00 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : .@..............
0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F : ...............?
0030: 00 00 00 -- -- -- -- -- -- -- -- -- -- -- -- -- : ................

");
			var obj = new Unit();

			obj.Guid = 0xF00C0EC40045F4D4;
			obj.ClearUpdateMask();
			obj.SetUpdateValue((UpdateFields)22, 0x0000003F);
			target.Add(obj);

			var pla = new Player();
			pla.IsKnown(obj);
			var actual = target.BuildUpdatePacket(pla);
			ArrayAssert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for BuildUpdateObject
		///</summary>
		[TestMethod()]
		public void BuildUpdateObjectTest2() {
			UpdateMgr target = new UpdateMgr();
			byte[] expected = PacketsHelper.GetBytes(@"

0000: 02 00 00 00 00 00 DF 2E 89 0C 1A 0C 30 F1 : ..............0.
0010: 08 00 00 03 00 00 40 00 00 00 00 00 00 00 00 00 : ......@.........
0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0030: 00 6E 34 B1 01 00 00 00 00 00 08 08 00 00 0F 6E : .n4............n
0040: 34 B1 01 38 00 00 40 00 00 40 00 00 00 00 00 00 : 4..2..@..@......
0050: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0080: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0090: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00A0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00B0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00C0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00D0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00E0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
00F0: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0100: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0110: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 : ................
0120: 00 00 00 00 62 00 00 00 08 08 08 00 -- -- -- -- : ............

");

			var obj1 = new Unit();
			obj1.Guid = 0xF130000C1A0C892E;
			obj1.ClearUpdateMask();
			obj1.SetUpdateValue((UpdateFields)16, 0x01B1346E);
			obj1.SetUpdateValue((UpdateFields)17, 0x00000000);
			obj1.SetUpdateValue((UpdateFields)46, 0x00080800);
			target.Add(obj1);

			var obj2 = new Player();
			obj2.Guid = 0x0000000001B1346E;
			obj2.ClearUpdateMask();
			obj2.SetUpdateValue((UpdateFields)22, 0x00000062);
			obj2.SetUpdateValue((UpdateFields)46, 0x00080808);
			target.Add(obj2);

			var pla = new Player();
			pla.IsKnown(obj1);
			pla.IsKnown(obj2);
			byte[] actual = target.BuildUpdatePacket(pla);
			ArrayAssert.AreEqual(expected, actual);
		}
	}
}
