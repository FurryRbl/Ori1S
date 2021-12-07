using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x0200027C RID: 636
	internal class FAMData
	{
		// Token: 0x04000713 RID: 1811
		public FileSystemWatcher FSW;

		// Token: 0x04000714 RID: 1812
		public string Directory;

		// Token: 0x04000715 RID: 1813
		public string FileMask;

		// Token: 0x04000716 RID: 1814
		public bool IncludeSubdirs;

		// Token: 0x04000717 RID: 1815
		public bool Enabled;

		// Token: 0x04000718 RID: 1816
		public FAMRequest Request;

		// Token: 0x04000719 RID: 1817
		public Hashtable SubDirs;
	}
}
