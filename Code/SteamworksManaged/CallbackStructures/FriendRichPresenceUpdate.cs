using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000146 RID: 326
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 12)]
	public struct FriendRichPresenceUpdate
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		internal static FriendRichPresenceUpdate Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FriendRichPresenceUpdate>(data, dataSize);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		public SteamID SteamIDFriend
		{
			get
			{
				return this.steamIDFriend;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0000FAC9 File Offset: 0x0000DCC9
		public AppID GameAppID
		{
			get
			{
				return new AppID(this.gameAppID);
			}
		}

		// Token: 0x040005D2 RID: 1490
		private SteamID steamIDFriend;

		// Token: 0x040005D3 RID: 1491
		private uint gameAppID;
	}
}
