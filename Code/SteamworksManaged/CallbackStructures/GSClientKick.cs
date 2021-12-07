using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000106 RID: 262
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientKick
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x0000B6B8 File Offset: 0x000098B8
		internal static GSClientKick Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSClientKick>(data, dataSize);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0000B6C1 File Offset: 0x000098C1
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0000B6C9 File Offset: 0x000098C9
		public DenyReason DenyReason
		{
			get
			{
				return this.denyReason;
			}
		}

		// Token: 0x04000499 RID: 1177
		private SteamID steamID;

		// Token: 0x0400049A RID: 1178
		private DenyReason denyReason;
	}
}
