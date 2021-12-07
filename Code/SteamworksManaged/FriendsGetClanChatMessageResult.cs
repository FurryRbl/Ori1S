using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000021 RID: 33
	public struct FriendsGetClanChatMessageResult
	{
		// Token: 0x040000C1 RID: 193
		public int Result;

		// Token: 0x040000C2 RID: 194
		public string Text;

		// Token: 0x040000C3 RID: 195
		public ChatEntryType ChatEntryType;

		// Token: 0x040000C4 RID: 196
		public SteamID Sender;
	}
}
