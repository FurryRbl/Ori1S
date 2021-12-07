using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200012D RID: 301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotReady
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x0000C523 File Offset: 0x0000A723
		internal static ScreenshotReady Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ScreenshotReady>(data, dataSize);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0000C52C File Offset: 0x0000A72C
		public ScreenshotHandle Local
		{
			get
			{
				return this.local;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0000C534 File Offset: 0x0000A734
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04000527 RID: 1319
		private ScreenshotHandle local;

		// Token: 0x04000528 RID: 1320
		private Result result;
	}
}
