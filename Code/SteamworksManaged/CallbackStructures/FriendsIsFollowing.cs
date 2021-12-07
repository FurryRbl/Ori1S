using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014F RID: 335
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
	public struct FriendsIsFollowing
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0000FBB6 File Offset: 0x0000DDB6
		internal static FriendsIsFollowing Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FriendsIsFollowing>(data, dataSize);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0000FBBF File Offset: 0x0000DDBF
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0000FBC7 File Offset: 0x0000DDC7
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0000FBCF File Offset: 0x0000DDCF
		public bool IsFollowing
		{
			get
			{
				return this.isFollowing;
			}
		}

		// Token: 0x040005E7 RID: 1511
		private Result result;

		// Token: 0x040005E8 RID: 1512
		private SteamID steamID;

		// Token: 0x040005E9 RID: 1513
		private bool isFollowing;
	}
}
