using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace UWoW.Helper {
	public class KeyUtilities {
		#region Imported members

		[DllImport("wfk2.dll", EntryPoint = "FindKey")]
		public static extern bool ExtractKey(string name, byte[] key);

		#endregion

	}
}
