using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000EA RID: 234
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatUpdate
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00009A30 File Offset: 0x00007C30
		internal static LobbyChatUpdate Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyChatUpdate>(data, dataSize);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00009A39 File Offset: 0x00007C39
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00009A41 File Offset: 0x00007C41
		public SteamID SteamIDUserChanged
		{
			get
			{
				return this.steamIDUserChanged;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00009A49 File Offset: 0x00007C49
		public SteamID SteamIDMakingChange
		{
			get
			{
				return this.steamIDMakingChange;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00009A51 File Offset: 0x00007C51
		public ChatMemberStateChange ChatMemberStateChange
		{
			get
			{
				return (ChatMemberStateChange)this.chatMemberStateChange;
			}
		}

		// Token: 0x040003EC RID: 1004
		private SteamID steamIDLobby;

		// Token: 0x040003ED RID: 1005
		private SteamID steamIDUserChanged;

		// Token: 0x040003EE RID: 1006
		private SteamID steamIDMakingChange;

		// Token: 0x040003EF RID: 1007
		private int chatMemberStateChange;
	}
}
