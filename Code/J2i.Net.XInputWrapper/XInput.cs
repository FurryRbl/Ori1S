using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000005 RID: 5
	public static class XInput
	{
		// Token: 0x06000039 RID: 57
		[DllImport("xinput9_1_0.dll")]
		public static extern int XInputGetState(int dwUserIndex, ref XInputState pState);

		// Token: 0x0600003A RID: 58
		[DllImport("xinput9_1_0.dll")]
		public static extern int XInputSetState(int dwUserIndex, ref XInputVibration pVibration);

		// Token: 0x0600003B RID: 59
		[DllImport("xinput9_1_0.dll")]
		public static extern int XInputGetCapabilities(int dwUserIndex, int dwFlags, ref XInputCapabilities pCapabilities);
	}
}
