using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000150 RID: 336
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsEnumerateFollowingList
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0000FBD7 File Offset: 0x0000DDD7
		internal static FriendsEnumerateFollowingList Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FriendsEnumerateFollowingList>(data, dataSize);
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		public SteamID[] SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public int ResultReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x040005EA RID: 1514
		private Result result;

		// Token: 0x040005EB RID: 1515
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private SteamID[] steamID;

		// Token: 0x040005EC RID: 1516
		private int resultsReturned;

		// Token: 0x040005ED RID: 1517
		private int totalResultCount;
	}
}
