using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000014 RID: 20
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NewLaunchQueryParameters
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000344E File Offset: 0x0000164E
		internal static NewLaunchQueryParameters Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<NewLaunchQueryParameters>(data, dataSize);
		}

		// Token: 0x0400004F RID: 79
		private byte ballast;
	}
}
