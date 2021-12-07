using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamShutdown
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x00010287 File Offset: 0x0000E487
		internal static SteamShutdown Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SteamShutdown>(data, dataSize);
		}

		// Token: 0x04000645 RID: 1605
		private byte ballast;
	}
}
