using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000B1 RID: 177
	public struct NetworkingReadP2PPacketResult
	{
		// Token: 0x04000326 RID: 806
		public bool Result;

		// Token: 0x04000327 RID: 807
		public uint MsgSize;

		// Token: 0x04000328 RID: 808
		public SteamID SteamIDRemote;
	}
}
