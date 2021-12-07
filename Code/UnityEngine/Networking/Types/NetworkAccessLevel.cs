using System;
using System.ComponentModel;

namespace UnityEngine.Networking.Types
{
	// Token: 0x02000241 RID: 577
	[DefaultValue(NetworkAccessLevel.Invalid)]
	public enum NetworkAccessLevel : ulong
	{
		// Token: 0x0400092E RID: 2350
		Invalid,
		// Token: 0x0400092F RID: 2351
		User,
		// Token: 0x04000930 RID: 2352
		Owner,
		// Token: 0x04000931 RID: 2353
		Admin = 4UL
	}
}
