using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000145 RID: 325
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClanOfficerListResponse
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x0000FA91 File Offset: 0x0000DC91
		internal static ClanOfficerListResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ClanOfficerListResponse>(data, dataSize);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0000FA9A File Offset: 0x0000DC9A
		public SteamID SteamIDClan
		{
			get
			{
				return this.steamIDClan;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0000FAA2 File Offset: 0x0000DCA2
		public int Officers
		{
			get
			{
				return this.officers;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0000FAAA File Offset: 0x0000DCAA
		public bool Success
		{
			get
			{
				return this.success != 0;
			}
		}

		// Token: 0x040005CF RID: 1487
		private SteamID steamIDClan;

		// Token: 0x040005D0 RID: 1488
		private int officers;

		// Token: 0x040005D1 RID: 1489
		private byte success;
	}
}
