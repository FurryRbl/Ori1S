using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000025 RID: 37
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersConnected
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00003A00 File Offset: 0x00001C00
		internal static SteamServersConnected Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SteamServersConnected>(data, dataSize);
		}

		// Token: 0x040000CE RID: 206
		private byte ballast;
	}
}
