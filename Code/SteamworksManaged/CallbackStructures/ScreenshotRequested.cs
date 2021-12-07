using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200012E RID: 302
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotRequested
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0000C53C File Offset: 0x0000A73C
		internal static ScreenshotRequested Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ScreenshotRequested>(data, dataSize);
		}

		// Token: 0x04000529 RID: 1321
		private byte ballast;
	}
}
