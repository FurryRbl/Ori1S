using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000104 RID: 260
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientApprove
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0000B67E File Offset: 0x0000987E
		internal static GSClientApprove Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSClientApprove>(data, dataSize);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000B687 File Offset: 0x00009887
		public SteamID SteamIDClient
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000B68F File Offset: 0x0000988F
		public SteamID SteamIDOwner
		{
			get
			{
				return this.ownerSteamID;
			}
		}

		// Token: 0x04000494 RID: 1172
		private SteamID steamID;

		// Token: 0x04000495 RID: 1173
		private SteamID ownerSteamID;
	}
}
