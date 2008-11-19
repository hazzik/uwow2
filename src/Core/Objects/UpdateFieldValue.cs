using System;
using System.Runtime.InteropServices;

namespace Hazzik.Objects {
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct UpdateFieldValue {
		[FieldOffset(0)] public Byte UInt8_0;
		[FieldOffset(1)] public Byte UInt8_1;
		[FieldOffset(2)] public Byte UInt8_2;
		[FieldOffset(3)] public Byte UInt8_3;

		[FieldOffset(0)] public UInt16 UInt16_0;
		[FieldOffset(2)] public UInt16 UInt16_1;

		[FieldOffset(0)] public UInt32 UInt32;

		[FieldOffset(1)] public Single Single;
	}
}