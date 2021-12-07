using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000148 RID: 328
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedClanChatMsg
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x0000FAEF File Offset: 0x0000DCEF
		internal static GameConnectedClanChatMsg Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameConnectedClanChatMsg>(data, dataSize);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public SteamID SteamIDClanChat
		{
			get
			{
				return this.steamIDClanChat;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0000FB00 File Offset: 0x0000DD00
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0000FB08 File Offset: 0x0000DD08
		public int MessageID
		{
			get
			{
				return this.messageID;
			}
		}

		// Token: 0x040005D6 RID: 1494
		private SteamID steamIDClanChat;

		// Token: 0x040005D7 RID: 1495
		private SteamID steamIDUser;

		// Token: 0x040005D8 RID: 1496
		private int messageID;
	}
}
