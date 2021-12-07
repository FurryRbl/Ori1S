using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000E5 RID: 229
	public struct CloudGetUGCDetailsResult
	{
		// Token: 0x040003D7 RID: 983
		public bool result;

		// Token: 0x040003D8 RID: 984
		public AppID appIDSender;

		// Token: 0x040003D9 RID: 985
		public string nameSender;

		// Token: 0x040003DA RID: 986
		public int fileSizeSender;

		// Token: 0x040003DB RID: 987
		public SteamID creatorSender;
	}
}
