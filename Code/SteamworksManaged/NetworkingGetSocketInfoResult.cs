using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000B7 RID: 183
	public struct NetworkingGetSocketInfoResult
	{
		// Token: 0x04000335 RID: 821
		public bool Result;

		// Token: 0x04000336 RID: 822
		public SteamID SteamIDRemote;

		// Token: 0x04000337 RID: 823
		public SNetSocketState SocketStatus;

		// Token: 0x04000338 RID: 824
		public uint IpRemote;

		// Token: 0x04000339 RID: 825
		public ushort PortRemote;
	}
}
