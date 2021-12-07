using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyEnter
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x000099E3 File Offset: 0x00007BE3
		internal static LobbyEnter Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyEnter>(data, dataSize);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000099EC File Offset: 0x00007BEC
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000099F4 File Offset: 0x00007BF4
		public uint ChatPermissions
		{
			get
			{
				return this.chatPermissions;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000099FC File Offset: 0x00007BFC
		public bool Locked
		{
			get
			{
				return this.locked;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00009A04 File Offset: 0x00007C04
		public ChatRoomEnterResponse ChatRoomEnterResponse
		{
			get
			{
				return (ChatRoomEnterResponse)this.chatRoomEnterResponse;
			}
		}

		// Token: 0x040003E5 RID: 997
		private SteamID steamIDLobby;

		// Token: 0x040003E6 RID: 998
		private uint chatPermissions;

		// Token: 0x040003E7 RID: 999
		[MarshalAs(UnmanagedType.I1)]
		private bool locked;

		// Token: 0x040003E8 RID: 1000
		private uint chatRoomEnterResponse;
	}
}
