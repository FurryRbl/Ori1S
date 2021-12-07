using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014A RID: 330
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GameConnectedChatLeave
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x0000FB29 File Offset: 0x0000DD29
		internal static GameConnectedChatLeave Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameConnectedChatLeave>(data, dataSize);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0000FB32 File Offset: 0x0000DD32
		public SteamID SteamIDClanChat
		{
			get
			{
				return this.steamIDClanChat;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0000FB3A File Offset: 0x0000DD3A
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0000FB42 File Offset: 0x0000DD42
		public bool Kicked
		{
			get
			{
				return this.kicked;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0000FB4A File Offset: 0x0000DD4A
		public bool Dropped
		{
			get
			{
				return this.dropped;
			}
		}

		// Token: 0x040005DB RID: 1499
		private SteamID steamIDClanChat;

		// Token: 0x040005DC RID: 1500
		private SteamID steamIDUser;

		// Token: 0x040005DD RID: 1501
		[MarshalAs(UnmanagedType.I1)]
		private bool kicked;

		// Token: 0x040005DE RID: 1502
		[MarshalAs(UnmanagedType.I1)]
		private bool dropped;
	}
}
