using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000010 RID: 16
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputKeystroke
	{
		// Token: 0x0400005A RID: 90
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.I2)]
		public short VirtualKey;

		// Token: 0x0400005B RID: 91
		[FieldOffset(2)]
		[MarshalAs(UnmanagedType.I2)]
		public char Unicode;

		// Token: 0x0400005C RID: 92
		[FieldOffset(4)]
		[MarshalAs(UnmanagedType.I2)]
		public short Flags;

		// Token: 0x0400005D RID: 93
		[FieldOffset(5)]
		[MarshalAs(UnmanagedType.I2)]
		public byte UserIndex;

		// Token: 0x0400005E RID: 94
		[FieldOffset(6)]
		[MarshalAs(UnmanagedType.I1)]
		public byte HidCode;
	}
}
