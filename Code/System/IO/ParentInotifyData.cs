using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x02000286 RID: 646
	internal class ParentInotifyData
	{
		// Token: 0x0400075F RID: 1887
		public bool IncludeSubdirs;

		// Token: 0x04000760 RID: 1888
		public bool Enabled;

		// Token: 0x04000761 RID: 1889
		public ArrayList children;

		// Token: 0x04000762 RID: 1890
		public InotifyData data;
	}
}
