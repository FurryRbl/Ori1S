using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000149 RID: 329
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameConnectedChatJoin
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x0000FB10 File Offset: 0x0000DD10
		internal static GameConnectedChatJoin Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameConnectedChatJoin>(data, dataSize);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0000FB19 File Offset: 0x0000DD19
		public SteamID SteamIDClanChat
		{
			get
			{
				return this.steamIDClanChat;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0000FB21 File Offset: 0x0000DD21
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x040005D9 RID: 1497
		private SteamID steamIDClanChat;

		// Token: 0x040005DA RID: 1498
		private SteamID steamIDUser;
	}
}
