using System;

namespace Mono.Unix
{
	// Token: 0x02000009 RID: 9
	[Flags]
	public enum FileSpecialAttributes
	{
		// Token: 0x04000036 RID: 54
		SetUserId = 2048,
		// Token: 0x04000037 RID: 55
		SetGroupId = 1024,
		// Token: 0x04000038 RID: 56
		Sticky = 512
	}
}
