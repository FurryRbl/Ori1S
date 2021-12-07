using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000499 RID: 1177
	internal interface IInternalMessage
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002FE1 RID: 12257
		// (set) Token: 0x06002FE2 RID: 12258
		Identity TargetIdentity { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002FE3 RID: 12259
		// (set) Token: 0x06002FE4 RID: 12260
		string Uri { get; set; }
	}
}
