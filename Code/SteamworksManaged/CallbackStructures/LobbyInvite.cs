using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyInvite
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x000099C2 File Offset: 0x00007BC2
		internal static LobbyInvite Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyInvite>(data, dataSize);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000099CB File Offset: 0x00007BCB
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000099D3 File Offset: 0x00007BD3
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000099DB File Offset: 0x00007BDB
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x040003E2 RID: 994
		private SteamID steamIDUser;

		// Token: 0x040003E3 RID: 995
		private SteamID steamIDLobby;

		// Token: 0x040003E4 RID: 996
		private GameID gameID;
	}
}
