using System;
using System.Diagnostics.CodeAnalysis;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000129 RID: 297
	[Flags]
	[SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags")]
	public enum RemoteStoragePlatform
	{
		// Token: 0x04000511 RID: 1297
		None = 0,
		// Token: 0x04000512 RID: 1298
		Windows = 1,
		// Token: 0x04000513 RID: 1299
		OSX = 2,
		// Token: 0x04000514 RID: 1300
		PS3 = 4,
		// Token: 0x04000515 RID: 1301
		Reserved1 = 8,
		// Token: 0x04000516 RID: 1302
		Reserved2 = 16,
		// Token: 0x04000517 RID: 1303
		All = -1
	}
}
