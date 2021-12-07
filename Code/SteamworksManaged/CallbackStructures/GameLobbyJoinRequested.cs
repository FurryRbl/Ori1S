using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000143 RID: 323
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameLobbyJoinRequested
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x0000FA4F File Offset: 0x0000DC4F
		internal static GameLobbyJoinRequested Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameLobbyJoinRequested>(data, dataSize);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public SteamID LobbyID
		{
			get
			{
				return this.lobbyID;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public SteamID FriendID
		{
			get
			{
				return this.friendID;
			}
		}

		// Token: 0x040005C9 RID: 1481
		private SteamID lobbyID;

		// Token: 0x040005CA RID: 1482
		private SteamID friendID;
	}
}
