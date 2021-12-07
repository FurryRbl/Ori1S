using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000162 RID: 354
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCountry
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x0001025C File Offset: 0x0000E45C
		internal static IPCountry Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<IPCountry>(data, dataSize);
		}

		// Token: 0x04000642 RID: 1602
		private byte deadWeight;
	}
}
