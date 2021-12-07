using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000EB RID: 235
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatMsg
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x00009A59 File Offset: 0x00007C59
		internal static LobbyChatMsg Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyChatMsg>(data, dataSize);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00009A62 File Offset: 0x00007C62
		public SteamID SteamIDLobby
		{
			get
			{
				return this.steamIDLobby;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00009A6A File Offset: 0x00007C6A
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00009A72 File Offset: 0x00007C72
		public ChatEntryType ChatEntryType
		{
			get
			{
				return (ChatEntryType)this.chatEntryType;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00009A7A File Offset: 0x00007C7A
		public uint ChatID
		{
			get
			{
				return this.chatID;
			}
		}

		// Token: 0x040003F0 RID: 1008
		private SteamID steamIDLobby;

		// Token: 0x040003F1 RID: 1009
		private SteamID steamIDUser;

		// Token: 0x040003F2 RID: 1010
		private byte chatEntryType;

		// Token: 0x040003F3 RID: 1011
		private uint chatID;
	}
}
