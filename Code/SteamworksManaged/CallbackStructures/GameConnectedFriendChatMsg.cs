using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014D RID: 333
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedFriendChatMsg
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		internal static GameConnectedFriendChatMsg Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameConnectedFriendChatMsg>(data, dataSize);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0000FB85 File Offset: 0x0000DD85
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000FB8D File Offset: 0x0000DD8D
		public int MessageID
		{
			get
			{
				return this.messageID;
			}
		}

		// Token: 0x040005E2 RID: 1506
		private SteamID steamIDUser;

		// Token: 0x040005E3 RID: 1507
		private int messageID;
	}
}
