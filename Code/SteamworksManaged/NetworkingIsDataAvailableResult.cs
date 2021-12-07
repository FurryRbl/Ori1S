using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000B5 RID: 181
	public struct NetworkingIsDataAvailableResult
	{
		// Token: 0x0400032F RID: 815
		public bool Result;

		// Token: 0x04000330 RID: 816
		public uint MsgSize;

		// Token: 0x04000331 RID: 817
		public NetSocketHandle Socket;
	}
}
