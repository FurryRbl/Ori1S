using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000164 RID: 356
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICallCompleted
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x00010276 File Offset: 0x0000E476
		internal static SteamAPICallCompleted Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SteamAPICallCompleted>(data, dataSize);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001027F File Offset: 0x0000E47F
		public ulong AsyncCall
		{
			get
			{
				return this.hAsyncCall;
			}
		}

		// Token: 0x04000644 RID: 1604
		private ulong hAsyncCall;
	}
}
