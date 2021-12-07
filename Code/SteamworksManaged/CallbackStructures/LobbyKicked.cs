using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyKicked
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00009ABC File Offset: 0x00007CBC
		internal static LobbyKicked Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyKicked>(data, dataSize);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00009AC5 File Offset: 0x00007CC5
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00009ACD File Offset: 0x00007CCD
		public SteamID SteamIDAdmin
		{
			get
			{
				return this.steamIDAdmin;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00009AD5 File Offset: 0x00007CD5
		public bool KickedDueToDisconnect
		{
			get
			{
				return this.kickedDueToDisconnect == 1;
			}
		}

		// Token: 0x040003F9 RID: 1017
		private SteamID steamIDLobby;

		// Token: 0x040003FA RID: 1018
		private SteamID steamIDAdmin;

		// Token: 0x040003FB RID: 1019
		private byte kickedDueToDisconnect;
	}
}
