using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000096 RID: 150
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UGCRequestUGCDetailsResult
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000827B File Offset: 0x0000647B
		internal static UGCRequestUGCDetailsResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UGCRequestUGCDetailsResult>(data, dataSize);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00008284 File Offset: 0x00006484
		private UGCDetails Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x040002A4 RID: 676
		private UGCDetails details;
	}
}
