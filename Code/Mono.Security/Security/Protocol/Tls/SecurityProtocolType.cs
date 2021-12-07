using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000095 RID: 149
	[Flags]
	[Serializable]
	public enum SecurityProtocolType
	{
		// Token: 0x040002B8 RID: 696
		Default = -1073741824,
		// Token: 0x040002B9 RID: 697
		Ssl2 = 12,
		// Token: 0x040002BA RID: 698
		Ssl3 = 48,
		// Token: 0x040002BB RID: 699
		Tls = 192
	}
}
