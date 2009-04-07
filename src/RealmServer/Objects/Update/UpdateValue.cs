using System;
using System.Runtime.InteropServices;

namespace Hazzik.Objects.Update {
	[StructLayout(LayoutKind.Explicit)]
	internal struct UpdateValue {
		[FieldOffset(0)]
		public byte Uint8_0;
		[FieldOffset(1)]
		public byte Uint8_1;
		[FieldOffset(2)]
		public byte Uint8_2;
		[FieldOffset(3)]
		public byte Uint8_3;

		[FieldOffset(0)]
		public ushort UInt16_1;
		[FieldOffset(2)]
		public ushort UInt16_0;

		[FieldOffset(0)]
		public uint UInt32;

		[FieldOffset(0)]
		public float Single;
	}
}