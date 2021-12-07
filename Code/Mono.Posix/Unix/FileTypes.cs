using System;

namespace Mono.Unix
{
	// Token: 0x0200000A RID: 10
	public enum FileTypes
	{
		// Token: 0x0400003A RID: 58
		Directory = 16384,
		// Token: 0x0400003B RID: 59
		CharacterDevice = 8192,
		// Token: 0x0400003C RID: 60
		BlockDevice = 24576,
		// Token: 0x0400003D RID: 61
		RegularFile = 32768,
		// Token: 0x0400003E RID: 62
		Fifo = 4096,
		// Token: 0x0400003F RID: 63
		SymbolicLink = 40960,
		// Token: 0x04000040 RID: 64
		Socket = 49152
	}
}
