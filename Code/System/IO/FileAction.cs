using System;

namespace System.IO
{
	// Token: 0x0200027F RID: 639
	internal enum FileAction
	{
		// Token: 0x04000725 RID: 1829
		Added = 1,
		// Token: 0x04000726 RID: 1830
		Removed,
		// Token: 0x04000727 RID: 1831
		Modified,
		// Token: 0x04000728 RID: 1832
		RenamedOldName,
		// Token: 0x04000729 RID: 1833
		RenamedNewName
	}
}
