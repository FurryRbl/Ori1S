using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000E9 RID: 233
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyDataUpdate
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x00009A0C File Offset: 0x00007C0C
		internal static LobbyDataUpdate Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyDataUpdate>(data, dataSize);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00009A15 File Offset: 0x00007C15
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00009A1D File Offset: 0x00007C1D
		public SteamID SteamIDMember
		{
			get
			{
				return this.steamIDMember;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00009A25 File Offset: 0x00007C25
		public bool Success
		{
			get
			{
				return this.success == 1;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private SteamID steamIDLobby;

		// Token: 0x040003EA RID: 1002
		private SteamID steamIDMember;

		// Token: 0x040003EB RID: 1003
		private byte success;
	}
}
