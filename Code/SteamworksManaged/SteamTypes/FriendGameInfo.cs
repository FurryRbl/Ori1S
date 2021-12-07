using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000CC RID: 204
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendGameInfo
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x00009544 File Offset: 0x00007744
		internal static FriendGameInfo Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FriendGameInfo>(data, dataSize);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000954D File Offset: 0x0000774D
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00009555 File Offset: 0x00007755
		public uint GameIP
		{
			get
			{
				return this.gameIP;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000955D File Offset: 0x0000775D
		public ushort GamePort
		{
			get
			{
				return this.gamePort;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00009565 File Offset: 0x00007765
		public ushort QueryPort
		{
			get
			{
				return this.queryPort;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000956D File Offset: 0x0000776D
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x0400037B RID: 891
		private GameID gameID;

		// Token: 0x0400037C RID: 892
		private uint gameIP;

		// Token: 0x0400037D RID: 893
		private ushort gamePort;

		// Token: 0x0400037E RID: 894
		private ushort queryPort;

		// Token: 0x0400037F RID: 895
		private SteamID steamIDLobby;
	}
}
