using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000037 RID: 55
	[Map]
	[CLSCompliant(false)]
	[Flags]
	public enum DirectoryNotifyFlags
	{
		// Token: 0x040001AB RID: 427
		DN_ACCESS = 1,
		// Token: 0x040001AC RID: 428
		DN_MODIFY = 2,
		// Token: 0x040001AD RID: 429
		DN_CREATE = 4,
		// Token: 0x040001AE RID: 430
		DN_DELETE = 8,
		// Token: 0x040001AF RID: 431
		DN_RENAME = 16,
		// Token: 0x040001B0 RID: 432
		DN_ATTRIB = 32,
		// Token: 0x040001B1 RID: 433
		DN_MULTISHOT = -2147483648
	}
}
