using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000012 RID: 18
	public struct XInputVibration
	{
		// Token: 0x04000061 RID: 97
		[MarshalAs(UnmanagedType.I2)]
		public ushort LeftMotorSpeed;

		// Token: 0x04000062 RID: 98
		[MarshalAs(UnmanagedType.I2)]
		public ushort RightMotorSpeed;
	}
}
