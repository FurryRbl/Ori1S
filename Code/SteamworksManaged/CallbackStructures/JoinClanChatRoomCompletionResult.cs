using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014C RID: 332
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct JoinClanChatRoomCompletionResult
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x0000FB63 File Offset: 0x0000DD63
		internal static JoinClanChatRoomCompletionResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<JoinClanChatRoomCompletionResult>(data, dataSize);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		public SteamID SteamIDClanChat
		{
			get
			{
				return this.steamIDClanChat;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public ChatRoomEnterResponse ChatRoomEnterResponse
		{
			get
			{
				return this.chatRoomEnterResponse;
			}
		}

		// Token: 0x040005E0 RID: 1504
		private SteamID steamIDClanChat;

		// Token: 0x040005E1 RID: 1505
		private ChatRoomEnterResponse chatRoomEnterResponse;
	}
}
