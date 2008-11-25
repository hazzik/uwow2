using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
	[DebuggerNonUserCode]
	public static class ArrayAssert {
		public static void AreEqual(Array expected, Array actual) {
			if(!ReferenceEquals(expected, actual)) {
				if(expected.Length != actual.Length) {
					throw new AssertFailedException(string.Format("Array's has different lengths. Expected <{0}>, got <{1}>",
					                                              expected.Length, actual.Length));
				}
				for(int i = 0; i < expected.Length; i++) {
					if(!expected.GetValue(i).Equals(actual.GetValue(i))) {
						throw new AssertFailedException(string.Format("Array's are not equals at {0}; Expected <{1}>, got <{2}>", i,
						                                              expected.GetValue(i), actual.GetValue(i)));
					}
				}
			}
		}
	}
}