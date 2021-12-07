using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000EC RID: 236
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyGameCreated
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x00009A82 File Offset: 0x00007C82
		internal static LobbyGameCreated Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyGameCreated>(data, dataSize);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00009A8B File Offset: 0x00007C8B
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00009A93 File Offset: 0x00007C93
		public SteamID SteamIDGameServer
		{
			get
			{
				return this.steamIDGameServer;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00009A9B File Offset: 0x00007C9B
		public uint IP
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00009AA3 File Offset: 0x00007CA3
		public ushort Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x040003F4 RID: 1012
		private SteamID steamIDLobby;

		// Token: 0x040003F5 RID: 1013
		private SteamID steamIDGameServer;

		// Token: 0x040003F6 RID: 1014
		private uint ip;

		// Token: 0x040003F7 RID: 1015
		private ushort port;
	}
}
