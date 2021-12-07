using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000007 RID: 7
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputCapabilities
	{
		// Token: 0x0400001A RID: 26
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.I1)]
		private byte Type;

		// Token: 0x0400001B RID: 27
		[FieldOffset(1)]
		[MarshalAs(UnmanagedType.I1)]
		public byte SubType;

		// Token: 0x0400001C RID: 28
		[FieldOffset(2)]
		[MarshalAs(UnmanagedType.I2)]
		public short Flags;

		// Token: 0x0400001D RID: 29
		[FieldOffset(4)]
		public XInputGamepad Gamepad;

		// Token: 0x0400001E RID: 30
		[FieldOffset(16)]
		public XInputVibration Vibration;
	}
}
