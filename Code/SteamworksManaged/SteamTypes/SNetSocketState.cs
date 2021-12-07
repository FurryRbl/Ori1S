using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000005 RID: 5
	public enum SNetSocketState
	{
		// Token: 0x04000016 RID: 22
		Invalid,
		// Token: 0x04000017 RID: 23
		Connected,
		// Token: 0x04000018 RID: 24
		Initiated = 10,
		// Token: 0x04000019 RID: 25
		LocalCandidatesFound,
		// Token: 0x0400001A RID: 26
		ReceivedRemoteCandidates,
		// Token: 0x0400001B RID: 27
		ChallengeHandshake = 15,
		// Token: 0x0400001C RID: 28
		Disconnecting = 21,
		// Token: 0x0400001D RID: 29
		LocalDisconnect,
		// Token: 0x0400001E RID: 30
		TimeoutDuringConnect,
		// Token: 0x0400001F RID: 31
		RemoteEndDisconnected,
		// Token: 0x04000020 RID: 32
		ConnectionBroken
	}
}
