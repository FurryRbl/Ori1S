using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014E RID: 334
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
	public struct FriendsGetFollowerCount
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x0000FB95 File Offset: 0x0000DD95
		internal static FriendsGetFollowerCount Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FriendsGetFollowerCount>(data, dataSize);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0000FB9E File Offset: 0x0000DD9E
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0000FBA6 File Offset: 0x0000DDA6
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0000FBAE File Offset: 0x0000DDAE
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x040005E4 RID: 1508
		private Result result;

		// Token: 0x040005E5 RID: 1509
		private SteamID steamID;

		// Token: 0x040005E6 RID: 1510
		private int count;
	}
}
