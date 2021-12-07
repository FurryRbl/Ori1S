using System;

namespace System.Configuration
{
	// Token: 0x0200002B RID: 43
	[Flags]
	internal enum ConfigurationLockType
	{
		// Token: 0x04000085 RID: 133
		Attribute = 1,
		// Token: 0x04000086 RID: 134
		Element = 2,
		// Token: 0x04000087 RID: 135
		Exclude = 16
	}
}
