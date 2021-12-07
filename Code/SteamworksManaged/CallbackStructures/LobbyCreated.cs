using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000EF RID: 239
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyCreated
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00009AE0 File Offset: 0x00007CE0
		internal static LobbyCreated Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyCreated>(data, dataSize);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00009AE9 File Offset: 0x00007CE9
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00009AF1 File Offset: 0x00007CF1
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x040003FC RID: 1020
		private Result result;

		// Token: 0x040003FD RID: 1021
		private SteamID steamIDLobby;
	}
}
