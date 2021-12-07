using System;

namespace UnityEngine
{
	// Token: 0x020001EE RID: 494
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x04000771 RID: 1905
		None = 0,
		// Token: 0x04000772 RID: 1906
		Shift = 1,
		// Token: 0x04000773 RID: 1907
		Control = 2,
		// Token: 0x04000774 RID: 1908
		Alt = 4,
		// Token: 0x04000775 RID: 1909
		Command = 8,
		// Token: 0x04000776 RID: 1910
		Numeric = 16,
		// Token: 0x04000777 RID: 1911
		CapsLock = 32,
		// Token: 0x04000778 RID: 1912
		FunctionKey = 64
	}
}
